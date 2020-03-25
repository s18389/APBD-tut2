using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using tut2.Models;

namespace tut2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] arguments = { "Data/data.csv", "result.xml" , "xml" };

           // Example of wrong path for result file
           // arguments[1] = "result/.xml";


            string pathToCsvGiven = @arguments[0];
            string pathDestinationGiven = @arguments[1];
            var dataFormatGiven = arguments[2];

            string pathToLogFile = @"log.txt";
            using (StreamWriter logWriter = new StreamWriter("log.txt", false))
            {
                logWriter.WriteLine();
            }

            checkIfPathCorrect(pathDestinationGiven);

            var listOfStudents = new HashSet<Student>(new CustomComparer());
            var listOfStudies = new HashSet<StudiesList>(new CustomComparerStudiesName()); //list of studies with diffrent names 

            try
            {
                FileInfo fileInfo = new FileInfo(pathToCsvGiven);
                using (var streamReader = new StreamReader(fileInfo.OpenRead()))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');
                        if (columns.Length == 9)
                        {
                            var studies = new studies
                            {
                                name = columns[2],
                                mode = columns[3]
                            };
                            var student = new Student
                            {
                                fname = columns[0],
                                lname = columns[1],
                                studies = studies,
                                IndexNumber = columns[4],
                                birthdate = DateTime.Parse(columns[5]),
                                email = columns[6],
                                mothersName = columns[7],
                                fathersName = columns[8]
                            };

                            var studiesName = new StudiesList
                            {
                                name = columns[2]
                            };

                            if (!listOfStudents.Add(student))
                            {
                                string content = "Found duplicate of or null argument " + line;
                                File.AppendAllText(pathToLogFile, content + Environment.NewLine);
                            }
                            else
                            {
                                listOfStudents.Add(student);
                                listOfStudies.Add(studiesName);
                            }
                        }
                        else
                        {
                            string content = "Missing arguments in " + line;
                            File.AppendAllText(pathToLogFile, content + Environment.NewLine);
                        }
                    }
                }

                countNumberOfStudents(listOfStudents, listOfStudies);
                var university = new University()
                {
                    students = listOfStudents,
                    activeStudies = listOfStudies,
                    createdAt = DateTime.Now.ToString("dd/MM/yyyy"),
                    author = "Jakub Michalski"
                };

                using (FileStream fileStream = File.Open("result.xml", FileMode.Create))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(University));
                    xmlSerializer.Serialize(fileStream, university);
                }

            }
            catch(FileNotFoundException e)
            {
                using (StreamWriter logWriter = new StreamWriter("log.txt", false))
                {
                    logWriter.WriteLine(e.Message + Environment.NewLine + "File does not exis" + Environment.NewLine);
                }
                Console.WriteLine(e);
            }
          
        }


        public static void checkIfPathCorrect(string pathDestinationGiven)
        {
            Regex regex = new Regex(@"((?:[a-zA-Z]\\:){0,1}(?:[\\/][\\w.]+){1,})");
            if (regex.IsMatch(pathDestinationGiven))
            {
                using (StreamWriter logWriter = new StreamWriter("log.txt", false))
                {
                    logWriter.WriteLine("The path is incorrect" + Environment.NewLine);
                }
                System.Environment.Exit(1);
            }
        }

        public static void countNumberOfStudents(HashSet<Student> listOfStudents, HashSet<StudiesList> listOfStudiesName)
        {
            foreach (StudiesList studiesList in listOfStudiesName)
            {
                int studiesCount = 0;
                foreach (Student student in listOfStudents)
                {
                    if (student.studies.name.Equals(studiesList.name))
                    {
                        studiesCount++;
                        studiesList.numberOfStudents = studiesCount;
                    }
                }
            }
        }

    }
}
