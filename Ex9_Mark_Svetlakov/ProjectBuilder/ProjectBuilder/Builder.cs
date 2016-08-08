using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectBuilder
{
    class Builder
    {
        public void BuildProjects()
        {
            string projectName;

            var projectOne = new Task(() =>
            {
                projectName = "Project One";
                BuildSchema(projectName);
            });


            var projectTwo = new Task(() =>
            {
                projectName = "Project Two";
                BuildSchema(projectName);
            });


            var projectThree = new Task(() =>
            {
                projectName = "Project Three";
                BuildSchema(projectName);
            });


            var projectFour = projectOne.ContinueWith(t =>
            {
                projectName = "Project Four";
                BuildSchema(projectName);
            });


            var projectFive = Task.WhenAll(projectOne, projectTwo, projectThree).ContinueWith(t =>
            {
                projectName = "Project Five";
                BuildSchema(projectName);
            });


            var projectSix = Task.WhenAll(projectThree, projectFour).ContinueWith(t2 =>
            {
                projectName = "Project Six";
                BuildSchema(projectName);
            });


            var projectSeven = Task.WhenAll(projectSix, projectFive).ContinueWith(t2 =>
            {
                projectName = "Project Seven";
                BuildSchema(projectName);
            });


            var projectEight = projectFive.ContinueWith(t =>
            {
                projectName = "Project Eight";
                BuildSchema(projectName);
            });


            var dependencies = new Task[] {projectOne, projectTwo, projectThree, projectFour, projectFive, projectSix, projectSeven, projectEight };

            var finalTask = Task.Factory.ContinueWhenAll(dependencies, t =>
            {
                Console.WriteLine("Builded");
            });

            projectOne.Start();
            projectTwo.Start();
            projectThree.Start();
            finalTask.Wait();

            DisposeTasks(dependencies);

        }


        private void BuildSchema(string projectName)
        {
            Console.WriteLine($"Creating project: {projectName} ");
            Thread.Sleep(1000);
            Console.WriteLine($"Project {projectName} is created");
        }


        private void DisposeTasks(Task[] listOfTasks)
        {
            foreach (var item in listOfTasks)
            {
                item.Dispose();
            }
        }
    }
}
