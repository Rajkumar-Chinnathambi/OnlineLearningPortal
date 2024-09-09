let emailStatus = false;
let passwordStatus = false;
function EmailValid() {
    let email = document.getElementById('email').value.trim();
    let emailError = document.getElementById('email-msg');
    const pattern = /^[A-Za-z0-9@$_.]+@[a-z]+\.[a-z]+$/;
    if (email == "") {
        emailError.innerHTML = "Email empty";
        emailStatus= false;
    }
    else if (!pattern.test(email)) {
        emailError.innerHTML = "Email Invalid";
        emailStatus = false;
    }
    else {
        emailError.innerHTML = ""
        emailStatus = true;
    }
    submit();
}

function PasswordValid() {
    
    let pwd = document.getElementById('pwd').value.trim();
    let pwdError = document.getElementById('pwd-msg'); 


    if (pwd == "") {
        pwdError.innerHTML = "Password Empty";
        passwordStatus = false;
    }
    else {
        pwdError.innerHTML = "";
        passwordStatus = true;
    }
    submit();
}
function submit() {
    let loginBtn = document.getElementById('loginBtn')

    console.log(`Email Status :${emailStatus} Password status : ${passwordStatus}`)
    if (emailStatus && passwordStatus) {        
        loginBtn.disabled=false;
        loginBtn.style.backgroundColor = "green";
    }
    else {
        loginBtn.disabled=true;
        loginBtn.style.backgroundColor = "grey";
    }
}