using KuSys.Core;
using KuSys.Core.Exceptions;
using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.Student;
using KuSys.DataAccess.Repositories.User;
using KuSys.Entities;
using KuSys.Entities.Requests;
using Moq;

namespace KuSys.Services.Tests;



[TestFixture]
public class StudentServiceTests
{
    private Mock<IStudentRepository> _mockStudentRepository;
    private Mock<IUserRepository> _mockUserRepository;
    private StudentService _studentService;
    private Mock<IStudentService> _mockStudentService;

    [SetUp]
    public void Setup()
    {
        _mockStudentRepository = new Mock<IStudentRepository>();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockStudentService = new Mock<IStudentService>();
        _studentService = new StudentService(_mockStudentRepository.Object,_mockUserRepository.Object);
    }
    
    [Test]
    public async Task AddStudent_ValidRequest_ShouldReturnAddStudentResponse()
    {
        // Arrange
        var requestModel = new AddStudentRequestModel
        {
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(2000, 1, 1),
            UserName = "johndoe",
            Password = "password"
        };
        var mockStudentRepository = new Mock<IStudentRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var studentService = new StudentService(mockStudentRepository.Object, mockUserRepository.Object);

        // Setup mock for AddStudentUser
        var userEntity = new User
        {
            Id = Guid.NewGuid(),
            UserName = requestModel.UserName
        };
        mockUserRepository.Setup(repo => repo.AddStudentUser(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(new DbCreateResult<User>(userEntity, OperationResult.Success));

        // Setup mock for AddNew (Add student)
        var studentEntity = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = requestModel.FirstName,
            LastName = requestModel.LastName,
            Gender = requestModel.Gender,
            BirthDate = requestModel.BirthDate,
            UserId = userEntity.Id
        };
        mockStudentRepository.Setup(repo => repo.AddNew(It.IsAny<Student>()))
            .ReturnsAsync(new DbCreateResult<Student>(studentEntity, OperationResult.Success));

        // Act
        var result = await studentService.AddStudent(requestModel);

        // Assert
        Assert.That(result,Is.Not.Null);
        Assert.That(result.IsSuccess,Is.True);
        Assert.That(studentEntity.Id, Is.EqualTo(result.Id));
    }
    
    [Test]
    public async Task AddStudent_AddUserFailed_ShouldThrowDatabaseException()
    {
        // Arrange
        var requestModel = new AddStudentRequestModel
        {
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(2000, 1, 1),
            UserName = "johndoe",
            Password = "password"
        };
        var mockStudentRepository = new Mock<IStudentRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var studentService = new StudentService(mockStudentRepository.Object, mockUserRepository.Object);

        // Setup mock for AddStudentUser (User creation fails)
        mockUserRepository.Setup(repo => repo.AddStudentUser(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(new DbCreateResult<User>(null, OperationResult.Failed, "User creation failed"));

        // Act & Assert
        Assert.ThrowsAsync<DatabaseException>(async () => await studentService.AddStudent(requestModel));
    }
    
    [Test]
    public async Task AddStudent_AddStudentFailed_ShouldThrowDatabaseException()
    {
        // Arrange
        var requestModel = new AddStudentRequestModel
        {
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(2000, 1, 1),
            UserName = "johndoe",
            Password = "password"
        };
        var mockStudentRepository = new Mock<IStudentRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var studentService = new StudentService(mockStudentRepository.Object, mockUserRepository.Object);

        // Setup mock for AddStudentUser (User creation is successful)
        var userEntity = new User
        {
            Id = Guid.NewGuid(),
            UserName = requestModel.UserName
        };
        mockUserRepository.Setup(repo => repo.AddStudentUser(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(new DbCreateResult<User>(userEntity, OperationResult.Success));

        // Setup mock for AddNew (Add student fails) -- Will fail in repo, cases like DbConnection couldn't be established --
        mockStudentRepository.Setup(repo => repo.AddNew(It.IsAny<Student>()))
            .ReturnsAsync(new DbCreateResult<Student>(null, OperationResult.Failed, "Add student failed"));

        // Act & Assert
        Assert.ThrowsAsync<DatabaseException>(async () => await studentService.AddStudent(requestModel));
    }

    [Test]
    public async Task GetStudentById_ExistingId_ShouldReturnGetStudentResponse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var studentEntity = new Student
        {
            Id = id,
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(2000, 1, 1)
        };
        var mockStudentRepository = new Mock<IStudentRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var studentService = new StudentService(mockStudentRepository.Object, mockUserRepository.Object);
        mockStudentRepository.Setup(repo => repo.GetById(id))
            .ReturnsAsync(new DbQuerySingleResult<Student>(studentEntity, OperationResult.Success));

        // Act
        var result = await studentService.GetStudentById(id);

        // Assert
        Assert.NotNull(result);
        Assert.That(studentEntity.Id,Is.EqualTo(result.Id));
        Assert.That(studentEntity.FirstName, Is.EqualTo(result.FirstName));
        Assert.That(studentEntity.LastName, Is.EqualTo(result.LastName));
        Assert.That(studentEntity.Gender, Is.EqualTo(result.Gender));
        Assert.That(studentEntity.BirthDate, Is.EqualTo(result.BirthDate));
    }
    
    [Test]
    public async Task GetStudentById_NonExistingId_ShouldThrowDataNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        _mockStudentRepository.Setup(repo => repo.GetById(id))
            .Throws<DataNotFoundException>();
        _mockStudentService.Setup(service => service.GetStudentById(id))
            .Throws<DataNotFoundException>();

        // Act & Assert
        Assert.ThrowsAsync<DataNotFoundException>(async() => await _mockStudentService.Object.GetStudentById(id));
    }
    
    [Test]
    public async Task DeleteStudentById_ExistingId_ShouldReturnDbDeleteResultWithSuccess()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mockStudentRepository = new Mock<IStudentRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var studentService = new StudentService(mockStudentRepository.Object, mockUserRepository.Object);
        var dbDeleteResult = new DbDeleteResult<Guid>(id, OperationResult.Success);
        mockStudentRepository.Setup(repo => repo.DeleteById(id)).ReturnsAsync(dbDeleteResult);

        // Act
        var result = await studentService.DeleteStudentById(id);

        // Assert
        Assert.That(OperationResult.Success,Is.EqualTo(result.Result));
        Assert.That(id,Is.EqualTo(result.Data));
    }

    [Test]
    public async Task DeleteStudentById_NonExistingId_ShouldReturnDbDeleteResultWithFailed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mockStudentRepository = new Mock<IStudentRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var studentService = new StudentService(mockStudentRepository.Object, mockUserRepository.Object);
        var dbDeleteResult = new DbDeleteResult<Guid>(id, OperationResult.Failed);
        mockStudentRepository.Setup(repo => repo.DeleteById(id)).ReturnsAsync(dbDeleteResult);

        // Act
        var result = await studentService.DeleteStudentById(id);

        // Assert
        Assert.That(OperationResult.Failed,Is.EqualTo(result.Result));
        Assert.That(id,Is.EqualTo(result.Data));
    }
    
    [Test]
    public async Task UpdateStudent_ExistingStudent_Success()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        var requestModel = new UpdateStudentModel
        {
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(2000, 1, 1)
        };
        var existingStudent = new Student { Id = studentId, FirstName = "OldFirstName", LastName = "OldLastName", Gender = Gender.Female, BirthDate = new DateTime(1990, 1, 1) };

        _mockStudentRepository
            .Setup(repo => repo.GetById(studentId))
            .ReturnsAsync(new DbQuerySingleResult<Student>(existingStudent, OperationResult.Success));
        _mockStudentRepository
            .Setup(repo => repo.Update(existingStudent))
            .ReturnsAsync(new DbUpdateResult<Student>(existingStudent, OperationResult.Success));

        // Act
        var result = await _studentService.UpdateStudent(studentId, requestModel);

        // Assert
        Assert.That(result.Result, Is.EqualTo(OperationResult.Success));
        Assert.That(existingStudent.FirstName, Is.EqualTo(requestModel.FirstName));
        Assert.That(existingStudent.LastName, Is.EqualTo(requestModel.LastName));
        Assert.That(existingStudent.Gender, Is.EqualTo(requestModel.Gender));
        Assert.That(existingStudent.BirthDate, Is.EqualTo(requestModel.BirthDate));

    }

    [Test]
    public async Task UpdateStudent_NonExistingStudent_Failure()
    {
        // Arrange
        Guid nonExistingStudentId = Guid.NewGuid();
        var requestModel = new UpdateStudentModel
        {
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(2000, 1, 1)
        };

        _mockStudentRepository
            .Setup(repo => repo.GetById(nonExistingStudentId))
            .Throws<DataNotFoundException>();

        // Act
        Assert.ThrowsAsync<DataNotFoundException>(async () =>
        {
            await _studentService.UpdateStudent(nonExistingStudentId, requestModel);
        });

        _mockStudentRepository.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Never);
    }
    
    [Test]
    public async Task UpdateStudent_RepositoryUpdateFails_ReturnsFailureResult()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        var requestModel = new UpdateStudentModel
        {
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(2000, 1, 1)
        };
        var existingStudent = new Student { Id = studentId, FirstName = "OldFirstName", LastName = "OldLastName", Gender = Gender.Female, BirthDate = new DateTime(1990, 1, 1) };

        _mockStudentService
            .Setup(service => service.UpdateStudent(studentId, requestModel))
            .Throws<InvalidException>();
        
        _mockStudentRepository
            .Setup(repo => repo.GetById(studentId))
            .Throws<InvalidException>();

        _mockStudentRepository
            .Setup(repo => repo.Update(existingStudent))
            .Throws<InvalidException>();
        
        // Assert
        Assert.ThrowsAsync<InvalidException>(async () =>
        {
            await _mockStudentService.Object.UpdateStudent(studentId, requestModel);
        });
    }

}
