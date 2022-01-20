using System;
using System.Data;
using System.Globalization;
namespace project1_ajm19x // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To The Task Manager Application!\n");

            string _name = string.Empty;
            string _description = string.Empty;
            DateTime _deadline;
            int numIncomplete = 0;

            List<Task> task_list = new List<Task>();

            printMenu();

            var user_input = string.Empty;
            int current_selection = -1;
            while (true) {
                user_input = Console.ReadLine();
                if (int.TryParse(user_input, out current_selection) && (current_selection >= 1 && current_selection <= 7))
                {
                    if (current_selection == 1)
                    {
                        Console.WriteLine("Please Enter Task Name: ");
                        _name = Console.ReadLine();
                        while (string.IsNullOrEmpty(_name))
                        {
                            Console.WriteLine("Error: Please Enter The Task Name Again:");
                            _name = Console.ReadLine();
                        }

                        Console.WriteLine("Please Enter a Description: ");
                        _description = Console.ReadLine();
                        while (string.IsNullOrEmpty(_description))
                        {
                            Console.WriteLine("Error: Please Enter The Description Again:");
                            _description = Console.ReadLine();
                        }

                        Console.WriteLine("Please Enter a Deadline(MM/DD/YYYY): ");

                        while (DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy",
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out _deadline)
                                && DateTime.Compare(_deadline, DateTime.Now) < 0)
                        {
                            if (DateTime.Compare(_deadline, DateTime.Now) < 0)
                            {
                                Console.WriteLine($"Error: Date Must be Later Than ({DateTime.Now}):");
                            }
                            else
                                Console.WriteLine("Error: Incorrect Format Try Again (MM/DD/YYYY):");
                        }

                        task_list.Add(new Task() { Name = _name, Deadline = _deadline, Description = _description, IsCompleted = false });
                        numIncomplete++;
                        Console.WriteLine("Task Added Succesfully");
                    }
                    else if (current_selection == 2 && task_list.Count != 0)
                    {

                        Print(task_list, true); //print all tasks

                        Console.WriteLine($"Please Enter The Index of the Task you wish to delete (1-{task_list.Count}):");
                        int deletion_index = -1;

                        while (!int.TryParse(Console.ReadLine(), out deletion_index))
                        {
                            Console.WriteLine("Error: Please Try Again:");
                        }

                        if (!task_list[deletion_index - 1].IsCompleted)
                        {
                            numIncomplete--;
                        }

                        task_list.RemoveAt(deletion_index - 1);
                        Console.WriteLine("Task Succesfully Deleted");
                    }
                    else if (current_selection == 3 && task_list.Count != 0)
                    {
                        Print(task_list, true); //print all tasks
                        int edit_index = -1;
                        Console.WriteLine($"Please Enter The Index of the Task you wish to edit (1-{task_list.Count}):");
                        while (!int.TryParse(Console.ReadLine(), out edit_index))
                        {
                            Console.WriteLine("Error: Please Try Again");
                        }
                        Console.WriteLine("Which Component Do You Wish to Edit\n" +
                            "1. Name\n" +
                            "2. Description\n" +
                            "3. Deadline\n" +
                            "4. Mark Completed");
                        int edit_selection = -1;
                        while (!int.TryParse(Console.ReadLine(), out edit_selection) || edit_selection > 4 || edit_selection < 1)
                        {
                            Console.WriteLine("Error: Please Try Again");
                        }
                        if (edit_selection == 1)
                        {
                            Console.WriteLine("Please Enter The New Name: ");
                            _name = Console.ReadLine();
                            while (string.IsNullOrEmpty(_name))
                            {
                                Console.WriteLine("Error: Please Try Again");
                                _name = Console.ReadLine();
                            }
                            task_list[edit_index - 1].Name = _name;
                            Console.WriteLine("Name changed succesfully");
                        }
                        else if (edit_selection == 2)
                        {
                            Console.WriteLine("Please Enter The New Description: ");
                            _description = Console.ReadLine();
                            while (string.IsNullOrEmpty(_description))
                            {
                                Console.WriteLine("Error: Please Try Again");
                                _description = Console.ReadLine();
                            }
                            task_list[edit_index - 1].Description = _description;
                            Console.WriteLine("Description changed succesfully");
                        }
                        else if (edit_selection == 3)
                        {
                            Console.WriteLine("Please Enter The New Deadline(MM/DD/YYYY): ");
                            while (DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy",
                                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _deadline)
                                    && DateTime.Compare(_deadline, DateTime.Now) < 0)
                            {
                                if (DateTime.Compare(_deadline, DateTime.Now) < 0)
                                {
                                    Console.WriteLine("Error: Date Must be in the future please try again(MM/DD/YYYY):");
                                }
                                else
                                    Console.WriteLine("Error: Incorrect Format Try Again (MM/DD/YYYY):");
                            }
                            task_list[edit_index - 1].Deadline = _deadline;
                            Console.WriteLine("Deadline changed succesfully");
                        }
                        else if (edit_selection == 4)
                        {
                            if (numIncomplete == 0)
                            {
                                Console.WriteLine("Error: No Outstanding Tasks");
                            }
                            else
                            {
                                task_list[edit_index - 1].IsCompleted = true;
                                numIncomplete -= 1;
                                Console.WriteLine("Succesfully Marked as Complete");
                            }
                        }
                    }
                    else if (current_selection == 4 && task_list.Count != 0)
                    {
                        if (numIncomplete == 0)
                        {
                            Console.WriteLine("Error: No Outstanding Tasks to be Completed");
                        }
                        else
                        {
                            Print(task_list, false); //print all incomplete
                            Console.WriteLine($"Enter the index of the task you wish to complete(1-{task_list.Count}):");
                            int complete_index = -1;
                            while (!int.TryParse(Console.ReadLine(), out complete_index))
                            {
                                Console.WriteLine("Error: Please Try Again ");
                            }
                            task_list[complete_index - 1].IsCompleted = true;
                            numIncomplete -= 1;
                            Console.WriteLine("Succesfully Marked as Complete");
                        }
                    }
                    //below might be useful for a later date
                    //DataTable table = CreateTable(task_list);

                    else if (current_selection == 5 && task_list.Count != 0)
                    {
                        if (numIncomplete == 0)
                        {
                            Console.WriteLine("Error: No Outstanding Tasks");
                        }
                        else
                        {
                            Print(task_list, false); //false we print incomplete tasks
                        }
                    }
                    else if (current_selection == 6 && task_list.Count != 0)
                    {
                          Print(task_list, true); //if true we print all the tasks
                    }
                    else if (current_selection == 7)
                    {
                        break;
                    }
                    else {
                        Console.WriteLine("Error: No tasks");
                    }
                }
                else {
                    Console.WriteLine("Error: Please Try Again");
                }
                printMenu();
            }


        }

        static void printMenu() {
            Console.WriteLine("Please Select An Option From The Menu:");
            Console.WriteLine("1. Create a new task\n" +
                              "2. Delete an existing task\n" +
                              "3. Edit an existing task\n" +
                              "4. Complete a task\n" +
                              "5. List all outstanding(not complete) tasks\n" +
                              "6. List all tasks\n" +
                              "7. Exit\n");
                                
        }

        private static void Print(List<Task> task_list, bool all_tasks) {
            if (all_tasks)
            {
                Console.WriteLine("  {0,-20} {1,-20} {2,-20} {3,-20}",
                                  "Name", "Description", "Deadline", "Completed");
                Console.WriteLine("  {0,-20} {1,-20} {2,-20} {3,-20}",
                       "----", "-----------", "--------", "---------");
                int index = 1;
                foreach (Task task in task_list)
                {

                    Console.WriteLine($"{index}." + task);

                    index++;
                }
            }
            else{
                Console.WriteLine("  {0,-20} {1,-20} {2,-20} {3,-20}",
                                  "Name", "Description", "Deadline", "Completed");
                Console.WriteLine("  {0,-20} {1,-20} {2,-20} {3,-20}",
                        "----", "-----------", "--------", "---------");
                int index = 1;

                foreach (Task task in task_list)
                {

                    if (!task.IsCompleted)
                        Console.WriteLine($"{index}." + task);

                    index++;
                }
            }
        }
    }
}

