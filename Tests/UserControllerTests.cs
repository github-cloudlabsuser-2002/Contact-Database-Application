using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;

[TestClass]
public class UserControllerTests
{
    private UserController _controller;
    private List<User> _users;

    [TestInitialize]
    public void TestInitialize()
    {
        _users = new List<User>
        {
            new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
            new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" },
            new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" },
        };

        UserController.userlist = _users;
        _controller = new UserController();
    }

    [TestMethod]
    public void Index_ReturnsCorrectViewWithModel()
    {
        var result = _controller.Index() as ViewResult;

        Assert.IsNotNull(result);
        var model = result.Model as List<User>;
        Assert.AreEqual(_users.Count, model.Count);
    }

    [TestMethod]
    public void Details_ReturnsCorrectViewWithModel_WhenIdIsValid()
    {
        var result = _controller.Details(1) as ViewResult;

        Assert.IsNotNull(result);
        var model = result.Model as User;
        Assert.AreEqual(_users[0].Id, model.Id);
    }

    [TestMethod]
    public void Details_ReturnsHttpNotFound_WhenIdIsInvalid()
    {
        var result = _controller.Details(999);

        Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
    }

    [TestMethod]
    public void Edit_Get_ReturnsCorrectViewWithModel_WhenIdIsValid()
    {
        var result = _controller.Edit(1) as ViewResult;

        Assert.IsNotNull(result);
        var model = result.Model as User;
        Assert.AreEqual(_users[0].Id, model.Id);
    }

    [TestMethod]
    public void Edit_Get_ReturnsHttpNotFound_WhenIdIsInvalid()
    {
        var result = _controller.Edit(999);

        Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
    }

    [TestMethod]
    public void Edit_Post_UpdatesUserAndRedirects_WhenModelStateIsValid()
    {
        var user = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };

        var result = _controller.Edit(1, user) as RedirectToRouteResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.RouteValues["action"]);
        Assert.AreEqual(user.Name, UserController.userlist[0].Name);
        Assert.AreEqual(user.Email, UserController.userlist[0].Email);
    }

    [TestMethod]
    public void Edit_Post_ReturnsViewWithModel_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("error", "some error");

        var user = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };

        var result = _controller.Edit(1, user) as ViewResult;

        Assert.IsNotNull(result);
        var model = result.Model as User;
        Assert.AreEqual(user.Id, model.Id);
    }
}
