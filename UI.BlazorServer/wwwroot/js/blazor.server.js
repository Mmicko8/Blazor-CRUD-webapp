/*  <color palette> */

let color_primary = "#5CADA8";
let color_primary_light = "#85C1BE";
let color_danger = "#be2a2a";

/*  </color palette> */

function confirmRemove(title, text) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon: 'warning',
            iconColor: color_danger,
            showCancelButton: true,
            cancelButtonText: 'No, abort!',
            confirmButtonColor: color_danger,
            cancelButtonColor: color_primary,
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            resolve(result.isConfirmed);
        })
    });
}


function alertError(title, text) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon: 'error',
            iconColor: color_danger,
            confirmButtonColor: color_primary,
            confirmButtonText: 'Okay'
        }).then((result) => {
            resolve(result.isConfirmed);
        })
    });
}

function alertRefreshed(title) {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        iconColor: color_primary_light,
        title: title,
        showConfirmButton: false,
        timer: 1500
    })
}