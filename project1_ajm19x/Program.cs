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

            //Initialize all the empty variables we are going to need
            string _name = string.Empty;
            string _description = string.Empty;
            DateTime _deadline;
            int numIncomplete = 0;

            //List for tasks
            List<Task> task_list = new List<Task>();

            //Print the initial menu
            printMenu();

            //User input
            var user_input = string.Empty;
            //Current Selection from menu
            int current_selection = -1;

            //Unless the user inputs 7 we keep going
            while (true) {
                user_input = Console.ReadLine();
                if (int.TryParse(user_input, out current_selection) && (current_selection >= 1 && current_selection <= 7))
                {
                    //ADD task
                    if (current_selection == 1)
                    {
                        /*
                            Check to make sure the user doesnt input a null string
                            Same for description
                         */
                        Console.Write("Please Enter Task Name: ");
                        _name = Console.ReadLine();
                        while (string.IsNullOrEmpty(_name))
                        {
                            Console.Write("Error: Please Enter The Task Name Again: ");
                            _name = Console.ReadLine();
                        }

                        Console.Write("Please Enter a Description: ");
                        _description = Console.ReadLine();
                        while (string.IsNullOrEmpty(_description))
                        {
                            Console.Write("Error: Please Enter The Description Again: ");
                            _description = Console.ReadLine();
                        }

                        /*
                            Make sure the user puts in a DateTime Formatted with MM/DD/YYYY
                            This Date must also be in the future, that is later than
                            DateTime.Now
                         */
                        Console.Write("Please Enter a Deadline(MM/DD/YYYY): ");
                        while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy",
                                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _deadline)
                                    || (_deadline == DateTime.MinValue || DateTime.Compare(_deadline, DateTime.Now) < 0))
                        {
                            if (_deadline == DateTime.MinValue)
                            {
                                Console.Write("Error: Incorrect Format Please Try Again (MM/DD/YYYY): ");
                            }
                            else {
                                Console.WriteLine($"Error: Date Must be Later Than {DateTime.Now}");
                                Console.Write("Please Try Again (MM/DD/YYYY): ");
                            }
                        }

                        //ADD the new task to our task list
                        task_list.Add(new Task() { Name = _name, Deadline = _deadline, Description = _description, IsCompleted = false });
                        //ADD 1 to numIncomplete
                        numIncomplete++;
                        //Tell the user we have added the task
                        Console.WriteLine("Task Added Succesfully");
                    }
                    //Delete Task if there are tasks to be deleted
                    else if (current_selection == 2 && task_list.Count != 0)
                    {

                        Print(task_list, true); //print all tasks

                        //Prompt the user to input the index, displays the possible range
                        Console.Write($"Please Enter The Index of the Task you wish to delete (1-{task_list.Count}):");
                        int deletion_index = -1;

                        //Check the user input to make sure it is in bounds and an integer
                        while (!int.TryParse(Console.ReadLine(), out deletion_index))
                        {
                            if (deletion_index > task_list.Count || deletion_index < 1) {
                                Console.WriteLine("Error: index out of bounds");
                                Console.Write($"Please Try Again(1-{task_list.Count}): ");
                            }
                            else
                                Console.Write("Error: Not an integer, Please Try Again: ");

                        }

                        //If the task was not completed then we subtract 1 from numIncomplete
                        if (!task_list[deletion_index - 1].IsCompleted)
                        {
                            numIncomplete--;
                        }

                        //remove at deletion_index - 1
                        task_list.RemoveAt(deletion_index - 1);
                        //Prompt that the task has been deleted
                        Console.WriteLine("Task Succesfully Deleted");
                    }
                    //EDIT task if there is a task to be edited
                    else if (current_selection == 3 && task_list.Count != 0)
                    {
                        //print all tasks
                        Print(task_list, true);
                        int edit_index = -1;

                        //Prompt the user to select and index to edit
                        //Check to make sure the index is in bounds and of type INT
                        Console.Write($"Please Enter The Index of the Task you wish to edit (1-{task_list.Count}):");
                        while (!int.TryParse(Console.ReadLine(), out edit_index))
                        {
                            if (edit_index > task_list.Count || edit_index < 1)
                            {
                                Console.WriteLine("Error: index out of bounds");
                                Console.Write($"Please Try Again(1-{task_list.Count}): ");
                            }
                            else
                                Console.Write("Error: Not an integer, Please Try Again: ");
                        }

                        //Give the user an edit menu to select from
                        Console.WriteLine("Which Component Do You Wish to Edit\n" +
                            "1. Name\n" +
                            "2. Description\n" +
                            "3. Deadline\n" +
                            "4. Mark Completed");

                        //Check the user input
                        Console.Write("Enter Selection: ");
                        int edit_selection = -1;
                        while (!int.TryParse(Console.ReadLine(), out edit_selection) || edit_selection > 4 || edit_selection < 1)
                        {
                            Console.Write($"Error: {edit_selection} is not an option, Please Try Again: ");
                        }

                        //Ask the user for a new name
                        //Ofc check to make sure the input is correct
                        if (edit_selection == 1)
                        {

                            Console.Write("Please Enter The New Name: ");
                            _name = Console.ReadLine();
                            while (string.IsNullOrEmpty(_name))
                            {
                                Console.Write("Error: Please Try Again");
                                _name = Console.ReadLine();
                            }
                            task_list[edit_index - 1].Name = _name;
                            Console.WriteLine("Name changed succesfully");
                        }
                        //Ask the user for a new description
                        //Ofc check to make sure the input is correct
                        else if (edit_selection == 2)
                        {
                            Console.Write("Please Enter The New Description: ");
                            _description = Console.ReadLine();
                            while (string.IsNullOrEmpty(_description))
                            {
                                Console.Write("Error: Please Try Again: ");
                                _description = Console.ReadLine();
                            }
                            task_list[edit_index - 1].Description = _description;
                            Console.WriteLine("Description changed succesfully");
                        }
                        //Ask the user for a new deadline
                        //Ofc check to make sure the input is correct
                        else if (edit_selection == 3)
                        {
                            Console.Write("Please Enter The New Deadline(MM/DD/YYYY): ");
                            while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy",
                                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _deadline)
                                    || (_deadline == DateTime.MinValue || DateTime.Compare(_deadline, DateTime.Now) < 0))
                            {
                                if (_deadline == DateTime.MinValue)
                                {
                                    Console.Write("Error: Incorrect Format Please Try Again (MM/DD/YYYY): ");
                                }
                                else
                                {
                                    Console.WriteLine($"Error: Date Must be Later Than {DateTime.Now}");
                                    Console.Write("Please Try Again (MM/DD/YYYY): ");
                                }
                            }
                            task_list[edit_index - 1].Deadline = _deadline;
                            Console.WriteLine("Deadline changed succesfully");
                        }
                        //Mark the task as completed
                        else if (edit_selection == 4)
                        {
                            //Check to see if there are any tasks that can even be marked completed
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
                    //Mark task complete if there even is a task to be marked complete
                    else if (current_selection == 4 && task_list.Count != 0)
                    {
                        //Tell the user if there is no outstanding tasks
                        if (numIncomplete == 0)
                        {
                            Console.WriteLine("Error: No Outstanding Tasks to be Completed");
                        }
                        else
                        {
                            Print(task_list, false); //print all incomplete
                            //Prompt the user
                            Console.Write($"Enter the index of the task you wish to complete(1-{task_list.Count}): ");
                            int complete_index = -1;
                            //Check if user input is in bounds and of type INT
                            while (!int.TryParse(Console.ReadLine(), out complete_index))
                            {
                                if (complete_index > task_list.Count || complete_index< 1)
                                {
                                    Console.WriteLine("Error: index out of bounds");
                                    Console.Write($"Please Try Again(1-{task_list.Count}): ");
                                }
                                else
                                    Console.Write("Error: Not an integer, Please Try Again: ");
                            }

                            task_list[complete_index - 1].IsCompleted = true;
                            numIncomplete -= 1;
                            Console.WriteLine("Succesfully Marked as Complete");
                        }
                    }
                    //below might be useful for a later date
                    //DataTable table = CreateTable(task_list);

                    //Print all ourstanding tasks if the list has tasks
                    else if (current_selection == 5 && task_list.Count != 0)
                    {
                        //If no outstanding tasks tell the user
                        if (numIncomplete == 0)
                        {
                            Console.WriteLine("Error: No Outstanding Tasks");
                        }
                        else
                        {
                            Print(task_list, false); //false we print incomplete tasks
                        }
                    }
                    //Print all the tasks regardless of complete status
                    else if (current_selection == 6 && task_list.Count != 0)
                    {
                          Print(task_list, true); //if true we print all the tasks
                    }
                    else if (current_selection == 7)
                    {
                        break;
                    }
                    //if we get here then the list has no tasks
                    else {
                        Console.WriteLine("Error: No tasks");
                    }
                }
                //if we get here then the user entry must be wrong
                else {
                    Console.WriteLine("Error: Please Try Again");
                }
                printMenu();
            }


        }

        //Function to print the menu to the console
        static void printMenu() {
            Console.WriteLine("Please Select An Option From The Menu:");
            Console.WriteLine("1. Create a new task\n" +
                              "2. Delete an existing task\n" +
                              "3. Edit an existing task\n" +
                              "4. Complete a task\n" +
                              "5. List all outstanding(not complete) tasks\n" +
                              "6. List all tasks\n" +
                              "7. Exit\n");
            Console.Write("Enter Selection: ");
                                
        }

        //Print, if flag is true then we print all tasks
        //If the flag is false then we print incomplete tasks
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

