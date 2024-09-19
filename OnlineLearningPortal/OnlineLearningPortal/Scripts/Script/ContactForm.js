function namevalid() {
    let name = document.getElementById('name').value.trim();
    let nameError = document.getElementById('nameError');
    const pattern = /\D/;
    if (name == '') {
        nameError.innerText = 'Name empty';
    }
    else if (!pattern.test(name)) {
        nameError.innerText = 'Name cannot include number';
    }
    else {
        nameError.innerText = 'Valid'
    }

}
document.getElementById('nameError').innerText = 'hell0';
alert("sfjsdkl")