using System.Data;
using System.Data.SqlClient;

namespace StudentRecordManagementSystem.Models
{
    public class StudentDataAccessLayer: IStudentDataAccessLayer
    {
        private readonly IConfiguration _configuration;
       
        public StudentDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            var lstStudent = new List<Student>();
            using (var con = new SqlConnection(_configuration.GetConnectionString(name: "cName")))
            {
                var cmd = new SqlCommand("spGetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var student = new Student
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Mobile = rdr["Mobile"].ToString(),
                        Address = rdr["Address"].ToString()
                    };

                    lstStudent.Add(student);
                }
                con.Close();
            }
            return lstStudent;
        }

        public void AddStudent(Student student)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString(name: "cName"));
            var cmd = new SqlCommand("spAddStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateStudent(Student student)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString(name: "cName"));
            var cmd = new SqlCommand("spUpdateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Student GetStudentData(int? id)
        {
            Student student = new();

            using (var con = new SqlConnection(_configuration.GetConnectionString(name: "cName")))
            {
                string sqlQuery = "SELECT * FROM Student WHERE Id= " + id;
                var cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.Email = rdr["Email"].ToString();
                    student.Mobile = rdr["Mobile"].ToString();
                    student.Address = rdr["Address"].ToString();
                }
            }
            return student;
        }

        public void DeleteStudent(int? id)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString(name: "cName"));
            var cmd = new SqlCommand("spDeleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}

