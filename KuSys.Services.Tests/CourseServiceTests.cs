using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core;
using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.Course;
using KuSys.Entities;
using KuSys.Services.Interfaces;
using Mapster;
using Moq;

namespace KuSys.Services.Tests;

[TestFixture]
public sealed class CourseServiceTests
{
    private Mock<ICourseRepository> _mockCourseRepository;
    private ICourseService _courseService;

    [SetUp]
    public void SetUp()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _courseService = new CourseService(_mockCourseRepository.Object);
    }
    
    [Test]
    public async Task AddNewCourse_RepositoryFails_ReturnsFailedResponse()
    {
        // Arrange
        var requestModel = new NewCourseRequestModel
        {
            Name = string.Empty,
            Code = string.Empty
        };

        // Mock the response of the course repository with a failure result
        var failureResult = new DbCreateResult<Course>(null, OperationResult.Failed);
        _mockCourseRepository.Setup(r => r.AddNew(It.IsAny<Course>()))
            .ReturnsAsync(new DbCreateResult<Course>(new Course(), OperationResult.Failed));

        // Act
        var result = await _courseService.AddNewCourse(requestModel);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<CourseResponseModel>(result);
        Assert.IsFalse(result.IsSuccess); // Ensure the IsSuccess property is false for failure
    }

    [Test]
    public async Task AddNewCourse_NullRequest_ReturnsFailedResponse()
    {
        // Arrange
        NewCourseRequestModel requestModel = null;

        // Act
        var result = await _courseService.AddNewCourse(requestModel);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<CourseResponseModel>(result);
        Assert.IsFalse(result.IsSuccess); // Ensure the IsSuccess property is false for a null request
        // Add more assertions based on the expected behavior of your service method
    }

    [Test]
    public async Task AddNewCourse_ValidRequest_ReturnsCourseResponseModel()
    {
        // Arrange
        var requestModel = new NewCourseRequestModel
        {
            Code = "MAT-1",
            Name = "Mathematics"
        };

        // Mock the response of the course repository
        var course = requestModel.Adapt<Course>();
        _mockCourseRepository.Setup(r => r.AddNew(It.IsAny<Course>()))
            .ReturnsAsync(new DbCreateResult<Course>(course, OperationResult.Success));

        // Act
        var result = await _courseService.AddNewCourse(requestModel);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<CourseResponseModel>(result);
        Assert.IsTrue(result.IsSuccess);
    }

    [Test]
    public async Task GetAll_ValidRequest_ReturnsCourseListResponseModel()
    {
        // Arrange
        var requestModel = new GetCoursesRequestModel();

        // Mock the response of the course repository
        var courses = new List<Course>
        {
            new Course
            {
                Code = "MAT1",
                Name = "Mathematics"
            },
            new Course
            {
                Code = "PHY1",
                Name = "Physics"
            }
        };
        var getAllResult = new PagedResponse<Course>
        {
            Data = courses,
            RecordsCount = 2,
            PageCount = 1,
            PageNumber = 1,
            PageSize = 10
        };
        _mockCourseRepository.Setup(r => r.GetAll(requestModel)).ReturnsAsync(getAllResult);

        // Act
        var result = await _courseService.GetAll(requestModel);

        // Assert
        Assert.That(result,Is.Not.Null);
        Assert.IsInstanceOf<CourseListResponseModel>(result);
        Assert.That(result.Courses,Is.Not.Null);
        Assert.That(result.Courses.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetAll_EmptyData_ReturnsEmptyResponse()
    {
        // Arrange
        var requestModel = new GetCoursesRequestModel();
        // Mock an empty response from the course repository
        var emptyResult = new PagedResponse<Course>
        {
            Data = new List<Course>(),
            RecordsCount = 0,
            PageCount = 0,
            PageNumber = 1,
            PageSize = 10
        };
        _mockCourseRepository.Setup(r => r.GetAll(requestModel)).ReturnsAsync(emptyResult);

        // Act
        var result = await _courseService.GetAll(requestModel);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf<CourseListResponseModel>(result);
        Assert.That(result.Courses,Is.Not.Null);
        Assert.IsEmpty(result.Courses);
    }
}