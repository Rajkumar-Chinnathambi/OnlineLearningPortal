function FirstNameValid() {
    let firstname = document.getElementById("firstName").value.trim();
    let firstNameError = document.getElementById("firstNameError");
    const pattern = /\D/;
    if (firstname == "") {
        firstNameError.innerText = "First name empty";
        return false;
    }
    else if (!pattern.test(firstname)) {
        firstNameError.innerText = "First name does not include numbers";
        return false;
    }
    else {
        firstNameError.innerText = "";
        return true;
    }
}
function LastNameValid() {
    let lastname = document.getElementById("lastName").value.trim();
    let lastNameError = document.getElementById("lastNameError");
    const pattern = /\D/;
    if (lastname == "") {
        lastNameError.innerText = "Last name empty";
        return false;
    }
    else if (!pattern.test(lastname)) {
        lastNameError.innerText = "last name does not include numbers";
        return false;
    }
    else {
        lastNameError.innerText = "";
        return true;
    }
}

function UserNameValid() {
    let username = document.getElementById("userName").value.trim();
    let userNameError = document.getElementById("userNameError");
    if (username == "") {
        userNameError.innerText = "User Name Empty";
        return false;
    }
    else {
        userNameError.innerText = "";
        return true;
    }

}

function PasswordValid() {
    const pattern = /[a-z0-9A-Z][0-9]/;
    let password = document.getElementById("pwd").value.trim();
    let passwordError = document.getElementById("pwdError");
    if (password == "") {
        passwordError.innerText = "Password Empty";
        return false;
    }
    else if (password.length < 8) {
        passwordError.innerText = "Password must be more than 8 char";
        return false;
    }
    else if (!pattern.test(password)) {
        passwordError.innerText = "Password must include char,number,symbol";
        return false;
    }
    else {
        passwordError.innerText = "";
        return true;
    }
}

function EmailValid() {
    const pattern = /^[A-Za-z0-9$@.-_]+@[a-z]+\.[a-z]+$/;
    let email = document.getElementById("email").value.trim();
    let emailError = document.getElementById("emailError");
    if (email == "") {
        emailError.innerText = "Email Empty";
        return false;
    }
    else if (!pattern.test(email)) {
        emailError.innerText = "Email invalid";
        return false;
    }
    else {
        emailError.innerText = "";
        return true;
    }
}
function DobValid() {
    let dob = document.getElementById("date").value.trim();
    let dobError = document.getElementById("dateError");
    console.log(dob)
    if (dob != '') {
        dobError.innerText = '';
        return true;
    }
    else {
        dobError.innerText = 'Select Date of birth';
        return false;
    }
}
function GenderValid() {
    let gender = document.getElementById("gender").value.trim();
    let genderError = document.getElementById("genderError");
    if (gender != 'Choose Gender') {
        genderError.innerText = '';
        return true;
    }
    else {
        genderError.innerText = 'Select Gender';
        return false;
    }
}
function StateValid() {
    let state = document.getElementById("state").value.trim();
    let stateError = document.getElementById("stateError");
    if (state != 'Choose State') {
        stateError.innerText = '';
        return true;
    }
    else {
        stateError.innerText = 'Select State';
        return false;
    }
}
function CityValid() {
    let city = document.getElementById("city").value.trim();
    let cityError = document.getElementById("cityError");
    if (city != 'Choose City') {
        cityError.innerText = '';
        return true;
    }
    else {
        cityError.innerText = 'Select City';
        cityError.style.color = 'red';
        return false;
    }
}
function MobileValid() {
    let mobile = document.getElementById("mobile").value.trim();
    let mobileError = document.getElementById("mobileError");
    if (mobile == "") {
        mobileError.innerText = "Mobile Empty";
        return false;
    }
    else if (mobile.length != 10) {
        mobileError.innerText = "Mobile must be 10 digit";
        return false;
    }
    else {
        mobileError.innerText = "";
        return true;
    }
}
function AddressValid() {
    let address = document.getElementById("address").value.trim();
    let addresError = document.getElementById("addressError");
    if (address == "") {
        addresError.innerText = "Address Empty";
        return false;
    }
    else {
        addresError.innerText = "";
        return true;
    }
}
function togglePassword() {
    var pwdField = document.getElementById("pwd");
    var toggleIcon = document.getElementById("togglePwd");

    if (pwdField.type === "password") {
        pwdField.type = "text";
        toggleIcon.classList.remove("fa-eye-slash");
        toggleIcon.classList.add("fa-eye");
    } else {
        pwdField.type = "password";
        toggleIcon.classList.remove("fa-eye");
        toggleIcon.classList.add("fa-eye-slash");
        
    }
}
document.getElementById("submitBtn").addEventListener("click", RegisterSubmit)
function RegisterSubmit(e) {
    e.preventDefault();
    let fnameStatus = FirstNameValid();
    let lastNameStatus = LastNameValid();
    let userNameStatus = UserNameValid();
    let passwordStatus = PasswordValid();
    let emailStatus = EmailValid();
    let mobileStatus = MobileValid();
    let addressStatus = AddressValid();
    let genderStatus = GenderValid();
    let dobStatus = DobValid();
    let StateStatus = StateValid();
    let cityStatus = CityValid();
    let registerForm = document.getElementById('RegisterForm');
    if (fnameStatus && lastNameStatus && userNameStatus && passwordStatus && emailStatus && mobileStatus && addressStatus && genderStatus && dobStatus && StateStatus && cityStatus) {
        registerForm.setAttribute('action', "Register");
        registerForm.submit();
    }
    else {
        registerForm.setAttribute('action', "#");
    }
}