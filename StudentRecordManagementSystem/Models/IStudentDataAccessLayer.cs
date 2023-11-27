namespace StudentRecordManagementSystem.Models
{
    public interface IStudentDataAccessLayer
    {
        IEnumerable<Student> GetAllStudent();

        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int? id);

        Student GetStudentData(int? id);
    }
}
