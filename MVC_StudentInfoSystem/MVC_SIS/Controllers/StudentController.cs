using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            if (ModelState.IsValid)
            {
                StudentRepository.Add(studentVM.Student);
                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View("Add", studentVM);
            }
            
        }

        [HttpGet]
        public ActionResult Edit(int studentId)
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());
            viewModel.Student = StudentRepository.Get(studentId);

            if (viewModel.Student.Courses != null)
            {
                viewModel.SelectedCourseIds = StudentRepository.GetCourseIds(viewModel.Student.Courses);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentVM studentVM)
        {
            
            studentVM.Student.Courses = new List<Course>();
            foreach (var id in studentVM.SelectedCourseIds)
            {
                studentVM.Student.Courses.Add(CourseRepository.Get(id));
            }

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            if (ModelState.IsValid)
            {
                StudentRepository.Edit(studentVM.Student);
                StudentRepository.SaveAddress(studentVM.Student.StudentId, studentVM.Student.Address);
                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                studentVM.SetStateItems(StateRepository.GetAll());
                return View("Edit", studentVM);

            }
        }

        [HttpGet]
        public ActionResult Delete(int studentId)
        {
            var student = StudentRepository.Get(studentId);
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}