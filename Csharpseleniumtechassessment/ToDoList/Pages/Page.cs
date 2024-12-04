using Csharpseleniumtechassessment.ToDoList.Utilities;
using OpenQA.Selenium;
using System.Linq;

namespace Csharpseleniumtechassessment.ToDoList.Pages
{
    public class TodoPage
    {
        private IWebDriver _driver;

        public TodoPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement InputField => _driver.FindElement(By.CssSelector(".new-todo, [placeholder='What needs to be done?']"));
        private IWebElement ClearCompletedButton => DriverHelper.WaitForElement(By.ClassName("clear-completed"));
        private IWebElement FilterAll => _driver.FindElement(By.XPath("//a[text()='All']"));
        private IWebElement FilterActive => _driver.FindElement(By.XPath("//a[text()='Active']"));
        private IWebElement FilterCompleted => _driver.FindElement(By.XPath("//a[text()='Completed']"));
        private IWebElement TodoList => _driver.FindElement(By.ClassName("todo-list"));

        public IList<IWebElement> GetAllTodos()
        {
            return TodoList.FindElements(By.TagName("li"));
        }

        public void AddTodo(string todoText)
        {
            LoggingHelper.LogMessage($"Adding todo: {todoText}");
            InputField.SendKeys(todoText + Keys.Enter);
        }

        public bool IsTodoPresent(string todoText)
        {
            var todos = GetAllTodos();
            return todos.Any(todo => todo.Text.Contains(todoText));
        }

        public IWebElement GetTodoByText(string todoText)
        {
            return GetAllTodos().FirstOrDefault(todo => todo.Text.Contains(todoText));
        }

        public void MarkTodoCompleted(string todoText)
        {
            LoggingHelper.LogMessage($"Marking todo as completed: {todoText}");
            var todo = GetTodoByText(todoText);
            todo?.FindElement(By.ClassName("toggle")).Click();
        }

        public void MarkAllAsDone()
        {
            var downArrow = _driver.FindElement(By.ClassName("toggle-all"));
            downArrow.Click();
        }

        public void DeleteTodo(string todoText)
        {
            LoggingHelper.LogMessage($"Deleting todo: {todoText}");
            var todo = GetTodoByText(todoText); todo?.FindElement(By.ClassName("destroy")).Click();
        }

        public void ClearCompletedTodos()
        {
            LoggingHelper.LogMessage("Clearing completed todos.");
            ClearCompletedButton.Click();
        }

        public void FilterTodos(string filterType)
        {
            LoggingHelper.LogMessage($"Filtering todos by: {filterType}");
            switch (filterType.ToLower())
            {
                case "all":
                    FilterAll.Click();
                    break;
                case "active":
                    FilterActive.Click();
                    break;
                case "completed":
                    FilterCompleted.Click();
                    break;
            }
        }

        public string GetItemsLeftCounterText()
        {
            return _driver.FindElement(By.ClassName("todo-count")).Text;
        }
    }
}
