let courseNameStatus = false;
function CourseNameValid() {
    let coursename = document.getElementById("coursename").value.trim();
    let coursenameError = document.getElementById("coursenameError");
    if (coursename == "") {
        coursenameError.innerHTML = "Couse name empty";
        return true;
    }
    else {
        coursenameError.innerHTML = "";
        return false;
    }
}
function CourseCatagoryValid() {
    let coursecatagory = document.getElementById("coursecatagory").value.trim();
    let coursecatagoryError = document.getElementById("coursecatagoryError");
    if (coursecatagory == "") {
        coursecatagoryError.innerHTML = "Couse catagory empty";
        return true;
    }
    else {
        coursecatagoryError.innerHTML = "";
        return false;
    }
}
function CoursesrcValid() {
    let coursesrc = document.getElementById("coursesrc").value.trim();
    let coursesrcError = document.getElementById("coursesrcError");
    if (coursesrc == "") {
        coursesrcError.innerHTML = "Couse catagory empty";
        return true;
    }
    else {
        coursesrcError.innerHTML = "";
        return false;
    }
}
function CoursedescValid() {
    let Coursedesc = document.getElementById("coursedesc").value.trim();
    let CoursedescError = document.getElementById("coursedescError");
    if (Coursedesc == "") {
        CoursedescError.innerHTML = "Couse catagory empty";
        return true;
    }
    else {
        CoursedescError.innerHTML = "";
        return false;
    }
}