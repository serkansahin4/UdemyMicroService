using AutoMapper;
using MongoDB.Driver;
using Udemy.Services.Catalog.Dtos;
using Udemy.Services.Catalog.Models;
using Udemy.Services.Catalog.Settings;
using Udemy.Shared.Dtos;

namespace Udemy.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _mapper = mapper;
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName) ;
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses=await _courseCollection.Find(x => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.Id).FirstAsync();

                }
            }
            else { new List<Course>(); }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
           var course= await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course is null)
            {
                return Response<CourseDto>.Fail("Kurs Bulunamadı", 404);
            }
            course.Category = await _categoryCollection.Find(x => x.Id == course.Id).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }
        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x =>x.UserId==userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.Id).FirstAsync();

                }
            }
            else { new List<Course>(); }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var course = _mapper.Map<Course>(courseUpdateDto);
            
            var result = await _courseCollection.ReplaceOneAsync(x=>x.Id==course.Id,course);
            if (result is null)
            {
                return Response<NoContent>.Fail("Kurs Bulunamadı.", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string courseId)
        {
            var result=await _courseCollection.DeleteOneAsync(x => x.Id == courseId);
            if (result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Kurs Silinemedi", 404);
        }

    }
}
