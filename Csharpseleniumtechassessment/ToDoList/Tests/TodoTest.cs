using NUnit.Framework;
using Csharpseleniumtechassessment.ToDoList.TestData;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Csharpseleniumtechassessment.ToDoList.Utilities;
using Csharpseleniumtechassessment.ToDoList.Pages;


namespace Csharpseleniumtechassessment.ToDoList.Tests
{
    [TestFixture]
    public class TodoTests
    {
        private TodoPage _todoPage;

        [SetUp]
        public void Setup()
        {
            // For future refinement, set url in appsetting.json in future. For demo purpose no set up
            DriverHelper.InitializeDriver();
            DriverHelper.Driver.Navigate().GoToUrl("https://todomvc.com/examples/react/dist/");
            _todoPage = new TodoPage(DriverHelper.Driver);
        }

        [TearDown]
        public void Teardown()
        {
            DriverHelper.QuitDriver();
        }

        [Test]
        public void AddTodoTest()
        {
            string uniqueTodo = $"Task-{Guid.NewGuid()}";
            _todoPage.AddTodo(uniqueTodo);
            Assert.IsTrue(_todoPage.IsTodoPresent(uniqueTodo), "Todo was not added.");
        }

        [Test]
        //Assumption of 1 element to edit only
        public void EditTodoByDoubleClicking()
        {
            string originalTodo = "Original Task";
            string updatedTodo = "Updated Task";
            _todoPage.AddTodo(originalTodo);

            var todo = _todoPage.GetTodoByText(originalTodo);
            Actions actions = new Actions(DriverHelper.Driver);
            actions.DoubleClick(todo).Perform();


            //For Future development, to handle no element present and exception
            var editInput = DriverHelper.WaitForElement(By.ClassName("view"));
            actions.DoubleClick(editInput).Perform();
            actions.SendKeys(Keys.Delete);
            actions.DoubleClick(editInput).Perform();
            actions.SendKeys(updatedTodo + Keys.Enter);

            //Wait for element, page no load causing test fail
            DriverHelper.WaitForElement(By.CssSelector($"li:contains('{updatedTodo}')"));


            Assert.IsTrue(_todoPage.IsTodoPresent(updatedTodo), "The todo was not updated.");
            Assert.IsFalse(_todoPage.IsTodoPresent(originalTodo), "The original todo still exists.");
        }

        [Test]
        public void MarkAllTodosAsDone()
        {
            _todoPage.AddTodo("Testdo1");
            _todoPage.AddTodo("Testdo2");
            _todoPage.AddTodo("Testdo1");

            _todoPage.MarkAllAsDone();

            foreach (var todo in _todoPage.GetAllTodos())
            {
                Assert.IsTrue(todo.GetAttribute("class").Contains("completed"), "Not all todos are marked as completed.");
            }
        }

        [Test]
        public void ValidateItemsLeftCounter()
        {
            _todoPage.AddTodo("Testdo1");
            _todoPage.AddTodo("Testdo2");
            _todoPage.AddTodo("Testdo3");

            Assert.AreEqual("3 items left!", _todoPage.GetItemsLeftCounterText(), "Counter did not match expected value.");

            Assert.AreNotEqual("2 items left!", _todoPage.GetItemsLeftCounterText(), "Counter did not update correctly.");
        }


        // For future development
        //TEST ON: Deletion or removal for 1 todo 
        //TEST ON: Marking completed for individual item
        //TEST ON : Filtering TODOS


    }
}
