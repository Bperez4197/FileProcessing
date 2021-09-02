using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\bryce\OneDrive\C#\C# 2 class\FileProcessing\Accounts.txt";
            //Read the file
            FileReader fr = new FileReader();
            fr.ReadFromFile(filePath);
            
            //Write to the file if info was added by user
            FileWriter fw = new FileWriter();
            if (fw.AskForInfo())
            {
                fw.WriteToFile(filePath);
            }
            Console.ReadLine();
        }

        
        class FileWriter
        {
            public string AccountName { get; set; }
            public DateTime InvoiceDate { get; set; }
            public DateTime DueDate { get; set; }
            public decimal AmountDue { get; set; }

            public bool AskForInfo()
            {
                Console.Write("Would you like to enter additional data? (Y/N): ");
                string answer = Console.ReadLine();
                if(answer.ToUpper() == "Y")
                {
                    ///////////////////Account Name
                    Console.Write("Please enter the Account Name: ");
                    AccountName = Console.ReadLine();
                    ///////////////////Invoice Date
                    Console.Write("Please enter the Invoice Date (mm/dd/yyyy): ");
                    string iDate = Console.ReadLine();
                    DateTime dateValue;
                    while(!DateTime.TryParse(iDate, out dateValue))
                    {
                        Console.Write("You did not enter a valid date. Please try again: ");
                        iDate = Console.ReadLine();
                    }
                    InvoiceDate = dateValue;
                    /////////////////Due Date
                    Console.Write("Please enter the Due Date (mm/dd/yyyy): ");
                    string dDate = Console.ReadLine();
                    DateTime dateValueTwo;
                    while(!DateTime.TryParse(dDate, out dateValueTwo))
                    {
                        Console.Write("You did not enter a valid date. Please try again: ");
                        dDate = Console.ReadLine();
                    }
                    DueDate = dateValueTwo;
                    //////////////////Amount Due
                    Console.Write("Please enter the Amount Due (decimal values): ");
                    string aDue = Console.ReadLine();
                    decimal result;
                    while(!Decimal.TryParse(aDue, out result))
                    {
                        Console.Write("You did not enter a valid date. Please try again: ");
                        aDue = Console.ReadLine();
                    }
                    AmountDue = result;
                    //flag to make the program terminate if user enters anything other than 'y' || 'Y'
                    return true;
                }
                return false;
            }

            public void WriteToFile(string pathToFile)
            {
                try
                {
                    using (StreamWriter writer = File.AppendText(pathToFile))
                    {
                        
                        writer.WriteLine($"{AccountName},{InvoiceDate.ToString("MM/dd/yyyy")},{DueDate.ToString("MM/dd/yyyy")},{AmountDue}");
                    }
                    Console.WriteLine("Your data has been saved");
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                
            }
        }



        class FileReader
        {
            public void ReadFromFile(string pathToFile)
            {
                using (FileStream fileOpener = new FileStream(pathToFile, FileMode.Open))
                using (StreamReader reader = new StreamReader(fileOpener))
                {
                    //content is all the text in the file
                    string content = reader.ReadToEnd();
                    //split the content at the end of each line
                    List<string> lines = content.Split(new string[] { Environment.NewLine },
                        StringSplitOptions.RemoveEmptyEntries).ToList();
                    //lines is now a list of each line
                    Console.WriteLine(lines[0]); //AccountName,InvoiceDate,DueDate,AmountDue
                    for (int i = 1; i < lines.Count(); i++)
                    {
                        //lines[i] is each line, so seperate by ',' to get the CSVs
                        string[] lineContent = lines[i].Split(',');
                        //lineContent[0] = name lineContent[1] = invoice date and so on
                        Console.WriteLine($"Row {i}: Account Name: {lineContent[0]} Invoice Date: {lineContent[1]} Due Date: {lineContent[2]} Amount Due: {lineContent[3]}");
                    }

                  
                }
            }
        }
    }
}
