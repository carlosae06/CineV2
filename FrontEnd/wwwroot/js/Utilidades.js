
function confirmar(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si',
            cancelButtonText: 'No'
        }).then((result) => {
            resolve(result.isConfirmed);
        })
    });


}


function StorageEvent(dotnetHelper) {

    window.addEventListener('storage', function (event) {
        if (event.storageArea === localStorage) {
            // It's local storage
            dotnetHelper.invokeMethodAsync("VerificarLogueo");
           
        }
    }, false);
}

function timerInactivo(dotnetHelper)
{
    var timer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer()
    {
        clearTimeout(timer);
        timer = setTimeout(logout, 600000); //10 MIN
    }

    function logout()
    {
        dotnetHelper.invokeMethodAsync("Logout");

    }
}

