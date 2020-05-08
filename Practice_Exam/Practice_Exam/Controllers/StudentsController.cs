using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Practice_Exam.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Practice_Exam.Controllers
{
    public class StudentsController : ApiController
    {
        string connectStr = ConfigurationManager.ConnectionStrings["MYDB"].ConnectionString;

        [HttpPost]
        public string insertStudent(string fn, string ln, string email, int year, int month, int day, string gender)
        {
            try
            {

                DateTime birthDate = new DateTime(year, month, day);
                string success = "Successful!";
                string sqlInsertCommand;
                using (SqlConnection myConn = new SqlConnection(connectStr))
                {
                    Students addingStudents = new Students();

                    addingStudents.firstName = fn;
                    addingStudents.lastName = ln;
                    addingStudents.emailAddress = email;
                    addingStudents.birthDate = birthDate;
                    addingStudents.gender = gender;

                    myConn.Open();
                    sqlInsertCommand = "INSERT INTO Students" + "(FirstName, LastName,EmailAddress, BirthDate, Gender) VALUES ('" + addingStudents.firstName + "','" + addingStudents.lastName + "','" + addingStudents.emailAddress + "','" + addingStudents.birthDate + "','" + addingStudents.gender + "');";
                    SqlCommand myInsertCommand = new SqlCommand(sqlInsertCommand, myConn);
                    myInsertCommand.ExecuteNonQuery();
                    myConn.Close();

                }

                return success;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpPut]
        public string updateStudent(int id, string fn, string ln, string email, int year, int month, int day, string gender)
        {
            try
            {
                DateTime birthDate = new DateTime(year, month, day);
                string success = "Successful!";
                string sqlUpdateCommand;
                using (SqlConnection myConn = new SqlConnection(connectStr))
                {
                    Students updateStudents = new Students();
                    updateStudents.studentId = id;
                    updateStudents.firstName = fn;
                    updateStudents.lastName = ln;
                    updateStudents.emailAddress = email;
                    updateStudents.birthDate = birthDate;
                    updateStudents.gender = gender;

                    myConn.Open();
                    sqlUpdateCommand = "UPDATE Students " + "SET FirstName = '" + updateStudents.firstName + "', LastName = '" + updateStudents.lastName + "', EmailAddress = ' " + updateStudents.emailAddress + "', BirthDate = '" + updateStudents.birthDate + "', Gender = '" + updateStudents.gender + "' WHERE StudentId =" + updateStudents.studentId + ";";
                    SqlCommand myUpdateCommand = new SqlCommand(sqlUpdateCommand, myConn);
                    myUpdateCommand.ExecuteNonQuery();
                    myConn.Close();
                }

                return success;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpDelete]
        public string deleteStudent(int id)
        {
            try
            {
                string success = "Successful!";

                string sqlDeleteCommand;
                using (SqlConnection myConn = new SqlConnection(connectStr))
                {
                    Students deleteStudents = new Students();
                    deleteStudents.studentId = id;


                    myConn.Open();
                    sqlDeleteCommand = "DELETE FROM Students WHERE StudentId =" + deleteStudents.studentId + ";";
                    SqlCommand myDeleteCommand = new SqlCommand(sqlDeleteCommand, myConn);
                    myDeleteCommand.ExecuteNonQuery();
                    myConn.Close();


                }
                return success;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //SEARCH FUNTIONALITY

            [HttpGet]
        public List<Students> GetStudentsByLastName(string lastname)
        {
            try
            {

                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Students> listStudents = new List<Students>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Students WHERE LastName = '" + lastname+ "'";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader(); ;

                    while (myReader.Read())
                    {
                        Students tempStudents = new Students();
                        tempStudents.studentId = int.Parse(myReader["StudentId"].ToString());
                        tempStudents.firstName = myReader["FirstName"].ToString();
                        tempStudents.lastName = myReader["LastName"].ToString();
                        tempStudents.emailAddress = myReader["EmailAddress"].ToString();
                        tempStudents.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempStudents.gender = myReader["Gender"].ToString();

                        listStudents.Add(tempStudents);

                    }
                    myReader.Close();
                    myConnection.Close();
                    
                    return listStudents;
                }
            }
            catch (Exception ex)
            {
                List<Students> emptyList = new List<Students>();
                return emptyList;
            }

        }

        [HttpGet]
        public List<Students> GetStudentsByEmail(string email)
        {
            try
            {

                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Students> listStudents = new List<Students>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Students WHERE EmailAddress = '" + email + "'";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader(); ;

                    while (myReader.Read())
                    {
                        Students tempStudents = new Students();
                        tempStudents.studentId = int.Parse(myReader["StudentId"].ToString());
                        tempStudents.firstName = myReader["FirstName"].ToString();
                        tempStudents.lastName = myReader["LastName"].ToString();
                        tempStudents.emailAddress = myReader["EmailAddress"].ToString();
                        tempStudents.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempStudents.gender = myReader["Gender"].ToString();

                        listStudents.Add(tempStudents);

                    }
                    myReader.Close();
                    myConnection.Close();

                    return listStudents;
                }
            }
            catch (Exception ex)
            {
                List<Students> emptyList = new List<Students>();
                return emptyList;
            }

        }


        //REPORTING

        [HttpGet]
        public List<Students> GetStudentsByRange(int yearFrom, int monthFrom, int dayFrom , int yearTo , int monthTo, int dayTo)
        {
            try
            {
                
                DateTime dateFrom = new DateTime(yearFrom,monthFrom,dayFrom);
                DateTime dateTo = new DateTime(yearTo, monthTo, dayTo);



                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Students> listStudents = new List<Students>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Students WHERE BirthDate BETWEEN '" + dateFrom.ToString("yyyy-MM-dd") + "' AND '"+ dateTo.ToString("yyyy-MM-dd") + "';";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader(); 

                    while (myReader.Read())
                    {
                        Students tempStudents = new Students();
                        tempStudents.studentId = int.Parse(myReader["StudentId"].ToString());
                        tempStudents.firstName = myReader["FirstName"].ToString();
                        tempStudents.lastName = myReader["LastName"].ToString();
                        tempStudents.emailAddress = myReader["EmailAddress"].ToString();
                        tempStudents.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempStudents.gender = myReader["Gender"].ToString();

                        listStudents.Add(tempStudents);

                    }
                    myReader.Close();
                    myConnection.Close();

                    return listStudents;
                }
            }
            catch (Exception ex)
            {
                List<Students> emptyList = new List<Students>();
                return emptyList;
            }

        }





        [HttpGet]
        public List<Students> GetStudentsByGender(string gender)
        {
            try
            {

                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Students> listStudents = new List<Students>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Students WHERE Gender = '" + gender + "'";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader(); ;

                    while (myReader.Read())
                    {
                        Students tempStudents = new Students();
                        tempStudents.studentId = int.Parse(myReader["StudentId"].ToString());
                        tempStudents.firstName = myReader["FirstName"].ToString();
                        tempStudents.lastName = myReader["LastName"].ToString();
                        tempStudents.emailAddress = myReader["EmailAddress"].ToString();
                        tempStudents.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempStudents.gender = myReader["Gender"].ToString();

                        listStudents.Add(tempStudents);

                    }
                    myReader.Close();
                    myConnection.Close();

                    return listStudents;
                }
            }
            catch (Exception ex)
            {
                List<Students> emptyList = new List<Students>();
                return emptyList;
            }

        }
    }
}
