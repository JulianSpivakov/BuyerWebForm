using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Moq;
using BuyerWebForm.Controllers;
using BuyerWebForm.Data;
using BuyerWebForm.Models;
using Xunit;

namespace BuyerWebFormTests
{
    public class BuyerWebFormControllerTests
    {
        [Fact]
        public void CanGetEntries()
        {
            var testData = new List<WebFormFields>
            {
                new WebFormFields
                {
                    Address = "Address 1",
                    EmailAddress = "EmailAddress 1",
                    FirstName = "FirstName 1",
                    LastName = "LastName 1",
                    PhoneNumber = "PhoneNumber 1"
                },
                new WebFormFields
                {
                    Address = "Address 2",
                    EmailAddress = "EmailAddress 2",
                    FirstName = "FirstName 2",
                    LastName = "LastName 2",
                    PhoneNumber = "PhoneNumber 2"
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<WebFormFields>>();

            var mockContext = new Mock<WebFormContext>();
            mockContext.Setup(c => c.WebFormFields).Returns(mockDbSet.Object);
        }

        [Fact]
        public async Task CreateValidModelReturnsRedirectToIndex()
        {
            // Arrange
            var mockContext = new Mock<WebFormContext>();
            var mockDbSet = new Mock<DbSet<WebFormFields>>();

            mockContext.Setup(c => c.WebFormFields).Returns(mockDbSet.Object);

            var controller = new WebFormFieldsController(mockContext.Object);

            var testEntry = new WebFormFields
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            // Act
            var result = await controller.Create(testEntry);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            mockContext.Verify(c => c.Add(It.IsAny<WebFormFields>()), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateInvalidModelReturnsViewWithModel()
        {
            // Arrange
            var mockContext = new Mock<WebFormContext>();
            var controller = new WebFormFieldsController(mockContext.Object);

            controller.ModelState.AddModelError("FirstName", "First name is required");

            var testEntry = new WebFormFields
            {
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            // Act
            var result = await controller.Create(testEntry);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);   
            var model = Assert.IsAssignableFrom<WebFormFields>(viewResult.Model);  
            Assert.Equal(testEntry, model);                                   

            mockContext.Verify(c => c.Add(It.IsAny<WebFormFields>()), Times.Never);
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task DeleteReturnsNotFoundResultWhenIdIsNull()
        {

            var mockContext = new Mock<WebFormContext>();
            var controller = new WebFormFieldsController(mockContext.Object);
            // Arrange
            ObjectId? id = null;

            // Act
            var result = await controller.Delete(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
