function isFirstNameEmpty() {
    if (document.getElementById("txtFirstName").value == "") {
        return 'First name should not be empty';
    } else {
        return '';
    }
}

function isFirstNameIsInValid() {
    if (document.getElementById("txtFirstName").value.indexOf("@") != -1) {
        return 'First name should not contain @';
    } else {
        return '';
    }
}

function isLastNameIsInValid() {
    if (document.getElementById("txtLastName").value.length >= 5) {
        return 'Last name should not contain more than 5 characters length';
    } else {
        return '';
    }
}

function isSalaryEmpty() {
    if (document.getElementById("txtSalary").value == "") {
        return 'Salary should not be empty';
    } else {
        return '';
    }
}

function isSalaryInValid() {
    if (isNaN(document.getElementById("txtSalary").value)) {
        return 'Enter valid salary';
    } else {
        return '';
    }
}

function isValid() {
    var FirstNameEmptyMessage = isFirstNameEmpty();
    var FirstNameInValidMessage = isFirstNameIsInValid();
    var LastNameInValidMessage = isLastNameIsInValid();
    var SalaryEmptyMessage = isSalaryEmpty();
    var SalaryInValidMessage = isSalaryInValid();

    var FinalErrorMessages = "Errors: ";
    if (FirstNameEmptyMessage != "") {
        FinalErrorMessages += "\n" + FirstNameEmptyMessage;
    } else if (FirstNameInValidMessage != "") {
        FinalErrorMessages += "\n" + FirstNameInValidMessage;
    } else if (LastNameInValidMessage != "") {
        FinalErrorMessages += "\n" + LastNameInValidMessage;
    } else if (SalaryEmptyMessage != "") {
        FinalErrorMessages += "\n" + SalaryEmptyMessage;
    } else if (SalaryInValidMessage != "") {
        FinalErrorMessages += "\n" + SalaryInValidMessage;
    }

    if (FinalErrorMessages != "Errors: ") {
        alert(FinalErrorMessages);
        return false;
    }
    return true;
}