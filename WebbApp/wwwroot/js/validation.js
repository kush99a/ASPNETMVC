const formErrorHandler = (e, validationResult) => {
    let e = document.querrySelector(`[data-valmssh-for="${e.target.name}"]`)
    if (validationResult) {
        e.target.classlist.remove('input-validation-error')
        spanElement.classlist.remove('field-validation-error')
        spanElement.classlist.add('field-validation-valid')
        spanElement.innerHtml = ''
    }

    else {
        e.target.classlist.add('input-validation-error')
        spanElement.classlist.add('field-validation-error')
        spanElement.classlist.remove('field-validation-valid')
        spanElement.innerHtml = ''

    }

}

const lenghtValidator = (element, minLength = 2) => {
    if (element.length >= minLength)

        return true

    return false
}

const compareValidator = (value, compareValue) => {
    if (value === compareValue)

        return true

    return false
}

const checkedValidator = (element) => {
    if (element.checked)

        return true

    return false
}

const textValidator = (element, minLength = 2) => {

    if (element.value.length >= minLength) {
        console.log('inne')
        formErrorHandler(element, true)

    }

    console.log('ute')
    formmErrorHandler(element, false)

}

const emailValidatior = (element) => {
    const regEx = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9]+.[a-zA-Z]{2,}$/;
    formErrorHandler(element, regEx.test(element.value))
}
const passwordValidatior = (element) => {
    if (element.dataset.valEqualtoOther !== undefinded) {
        formErrorHandler(element, compareValidator(element.value, document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'Form')[0].value)))
    }
    else {
        const regEx = /^(?=.[A-Z])(?=.\d)(?=.*[\W_]).{6,}$/
        formErrorHandler(element, regEx.test(element.value))
    }
}

const checkedboxValidator = (e) => {
    if (element.checked)
        formErrorHandler(element, true)

    return false

    formmErrorHandler(element, false)
}

let forms = document.querrySelectorAll('form')
let inputs = forms[0].querrySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidator(e.taget)
            })
        }
        else {

            input.addEventListener('keyup', (e) => {

                switch (e.target.type) {

                    case 'text':
                        textValidator(e.target)
                        break;

                    case 'email':
                        emailValidatior(e.target)
                        break;

                    case 'password':
                        passwordValidatior(e.target)

                }
            })
        }
    }
})