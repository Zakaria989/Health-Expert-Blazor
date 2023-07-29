export function focusElement(element) {
    if (element){
        element.scrollIntoView({behavior:"smooth", block:"end",inline:"nearest"})
    }
}


