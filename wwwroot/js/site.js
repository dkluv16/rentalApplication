// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.navTrigger').click(function () {
    $(this).toggleClass('active');
    console.log("Clicked menu");
    $("#mainListDiv").toggleClass("show_list");
    $("#mainListDiv").fadeIn();

});
{

    $(window).scroll(function () {
        if ($(document).scrollTop() > 50) {
            $('.nav').addClass('affix');
            console.log("OK");
        } else {
            $('.nav').removeClass('affix');
        }
    });

}


function picker(...listOfDates) {
    document.getElementById("flatpickrcontainer").flatpickr({
        wrap: true,
        minDate: "today",
        dateFormat: "m-d-Y H:i",
        disable: [
           ...listOfDates
        ],
        weekNumbers: true,
        enableTime: true, // enables timepicker default is false
        time_24hr: false, // set to false for AM PM default is false
        onChange: function (selectedDates, dateStr, instance) {
            console.log("changed");
        }
    });

    document.getElementById("flatpickrcontainer1").flatpickr({
        wrap: true,
        minDate: "today",
        dateFormat: "m-d-Y H:i",
        disable:
            [
                ...listOfDates
            ],
        weekNumbers: true,
        enableTime: true, // enables timepicker default is false
        time_24hr: false, // set to false for AM PM default is false
        onChange: function (selectedDates, dateStr, instance) {
            console.log("changed");
        }
    });
}
function toggle(number) {
    var cont = document.getElementById(number + ' cont');
    
    if (cont.style.display == 'block') {
        cont.style.display = 'none';
        $('#' + number + 'num').val('');
        $('#' + number + 'bed').val('');
        
    
            }
            else {
                cont.style.display = 'block';
        $('#' + number + 'num').val('1');
        $('#' + number + 'bed').val('1');
       
    }

}


function show_hide_row(row) {
    $('#' + row + 'hidden_row').toggle();
}


function editField(number) {
   
    $('#' + number + 'edit').prop('disabled', (i, v) => !v);
    $('#' + number + 'edited').prop('disabled', (i, v) => !v);
    $('#' + number + 'beddingEdit').prop('disabled', (i, v) => !v);
}
function PrintPartOfPage(dvprintid) {
    var prtContent = document.getElementById(dvprintid);
    var WinPrint = window.open('', '', 'letf=100,top=100,width=600,height=600');
    WinPrint.document.write('<html><head><title>' + document.title + '</title>');
    WinPrint.document.write('</head><body >');
    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.write('</body></html>');
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();   
    mywindow.close();
}
