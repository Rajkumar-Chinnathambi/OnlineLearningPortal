
function EmailValid() {
    let email = document.getElementById('email').value.trim();
    let emailError = document.getElementById('email-msg');
    const pattern = /^[A-Za-z0-9@$_.]+@[a-z]+\.[a-z]+$/;
    if (email == "") {
        emailError.innerHTML = "Email empty";
        return false;
    }
    else if (!pattern.test(email)) {
        emailError.innerHTML = "Email Invalid";
        return false;
    }
    else {
        emailError.innerHTML = ""
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
function PasswordValid() {
    
    let pwd = document.getElementById('pwd').value.trim();
    let pwdError = document.getElementById('pwd-msg'); 


    if (pwd == "") {
        pwdError.innerHTML = "Password Empty";
        return false;
    }
    else {
        pwdError.innerHTML = "";
        return true;
    }
}

let loginBtn = document.getElementById('loginBtn')
loginBtn.addEventListener('click', (e) => {
    e.preventDefault();
    let loginForm = document.getElementById('LoginForm')
    let emailStatus = EmailValid();
    let passwordStatus = PasswordValid();

    console.log(`Email Status :${emailStatus} Password status : ${passwordStatus}`)
    if (emailStatus && passwordStatus) {
        loginForm.setAttribute('action', "Index");
        loginForm.submit();
    }
    else {
        loginForm.setAttribute('action', "#");
    }
})
