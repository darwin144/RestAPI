$.ajax({
    url: "https://localhost:7258/RestAPI/Employee",
    success: (result) => {
        console.log(result);
    }
}).done((result) => {
    //select element
    let data = [];
    $.each(result.data, (key, val) => {
        data.push([
            `${key+1}.`,
            val.nik,
            val.firstName,
            val.lastName,
            val.email,
            val.birthDate,
            `<button class="btn btn-primary btn-update" data-toggle="modal" data-bs-target="#employeeModalUpdate" onclick="updateFormEmployee('${val?.guid}','${val.nik}','${val.firstName}','${val.lastName}','${val.email}','${val.birthDate}','${val.gender}','${val.phoneNumber}')">Update </button>
            <button class="btn btn-danger btn-delete" data-toggle="modal" data-bs-target="#universityModal" onclick="DeleteEmployee('${val.guid}')">Delete </button>`
        ]);
    });
    $(document).ready(function () {
        $('#star-war').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            data: data,
            columns: [
                { title: 'NO' },
                { title: 'NIK' },
                { title: 'FIRSTNAME' },
                { title: 'LASTNAME' },
                { title: 'EMAIL' },
                { title: 'BIRTHDATE' },
                { title: 'ACTION'}
            ]
        });

        $('star-war').on('click', '.btn-create', function () {
            openEmployeeModal2();
        });
        $('star-war').on('click', '.btn-delete', function () {
            DeleteEmployee(guid);
        });
    });
    
});



function openEmployeeModal2() {
    $('#employeeModal2').modal('show');
}

function closeModal2() {
    $('#employeeModal2').modal('hide');
}

// method post

function submitFormEmployee() {
    $('#employeeModal2').submit(function (event) {
        event.preventDefault();

        //Fetch the values from the input elements  
        var eFirstname = $('#firstName').val();
        var eLastname = $('#lastName').val();
        var eBirthdate = $('#birthDate').val();
        var egender = $("input[name='gender']:checked").val() === 1 ? 1 : 0;
        var eHiringdate = $('#hiringDate').val();
        var eEmail = $('#email').val();
        var ePhonenumber = $('#phoneNumber').val();
        var eMajor = $('#major').val();  
        var eDegree = $('#degree').val();
        var eGPA = $('#gpa').val();
        var eCode = $('#code').val();
        var eName = $('#name').val();
        var ePassword = $('#password').val();
        var eConfirmPassword = $('#confirmPassword').val();
       
        $.ajax({
            async: true,
            url: "https://localhost:7258/RestAPI/Account/Register",
            method: 'POST',
            data: JSON.stringify({
                '__metadata': {
                    'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
                },
                // Pass the parameters
                'firstName': eFirstname,
                'lastName': eLastname,
                'birthDate': eBirthdate,
                'gender': egender,
                'hiringDate': eHiringdate,
                'email': eEmail,
                'phoneNumber': ePhonenumber,
                'major': eMajor,
                'degree': eDegree,
                'gpa': eGPA,
                'code': eCode,
                'name': eName,
                'password': ePassword,
                'confirmPassword': eConfirmPassword
            }),
            headers : {
                "accept": "application/json;odata=verbose",    
                "content-type": "application/json;odata=verbose", 
                "X-RequestDigest": $("#__REQUESTDIGEST").val()
            },

            success: function (data) {
                swal("Item created successfully", "success");
            },
            error: function (xhr, status, error) {
                console.log(error);
                // Tangani kesalahan yang terjadi
            }
        });
    })
}


// update employee

function updateFormEmployee(guid, nik, firstName, lastName, birthDate, email, phoneNumber, gender) {
    document.getElementById('nik-update').value = nik;
    document.getElementById('firstName-update').value = firstName;
    document.getElementById('lastName-update').value = lastName;
    document.getElementById('birthDate-update').value = birthDate;
    document.getElementById('email-update').value = email;
    document.getElementById('phoneNumber-update').value = phoneNumber;

    if (gender === 1) {
        document.getElementById('gender-l').checked = true;
    } else {
        document.getElementById('gender-p').checked = true;
    }

    // Change the modal title
    document.getElementById('employeeModalLabel').innerText = 'Update Employee';

    // Show the modal
    $('#employeeModalUpdate').modal('show');

    // Add an event listener to the form submit button for updating the employee
    document.getElementById('formEmployeeUpdate').addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form submission

        // Call the updateEmployee function with the GUID parameter
        updateEmployee(guid);
    });

}

function updateEmployee(guid) {

    var eNik = document.getElementById('nik-update').value;
    var eFirst = document.getElementById('firstName-update').value;
    var eLast = document.getElementById('lastName-update').value;
    var eBDate = document.getElementById('birthDate-update').value;
    var eGender = document.querySelector('input[name="gender-update"]:checked').id.includes('m') ? 1 : 0;
    var eEmail = document.getElementById('email-update').value;
    var ePhone = document.getElementById('phoneNumber-update').value;


    $.ajax({
        async: true,
        url: "https://localhost:7258/RestAPI/Employee",
        method: 'PUT',
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem'
            },
            'guid': guid,
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-HTTP-Method": "MERGE",
            "IF-MATCH": "*"
        },
        success: function (data) {
            console.log(data);
            window.location.reload()
            // Get the updated row data as an array
            var updatedRowData = [
                eNik,
                eFirst,
                eLast,
                eBDate,
                eGender === 1 ? 'Male' : 'Female',
                eEmail,
                ePhone
                // Add other columns as needed
            ];

            // Loop through each row in the DataTable
            dataTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var rowData = this.data();

                // Check if the GUID in the row matches the guid parameter
                if (rowData[0] === guid) {
                    // Update the row data
                    this.data(updatedRowData);

                    // Redraw the updated row
                    this.invalidate();

                    // Exit the loop
                    return false;
                }
            });
            $('#employeeUpdateModal').modal('hide');
        },
        error: function (error) {
            console.log("Error: " + JSON.stringify(error));
        }
    });
}


// delete employee
function DeleteEmployee(guid) {

    $.ajax({
        async: true,
        url: `https://localhost:7258/RestAPI/Employee/${guid}`,
        method: 'DELETE',        
        success: function (result) {
            console.log(result);
            window.location.reload();
           
        },
        error: function (error) {
            console.log("Error: " + JSON.stringify(error));
        }
    });
}

    // modal

    /*function openModal(url) {
        $.ajax({
            url: url,
            success: (result) => {
                console.log(result);
            }
        }).done((pokemon) => {
    
            let modalBody = $('#pokemonModalBody');
            modalBody.empty();
            let heroName = $('#pokemonModalLabel');
            heroName.empty();
            heroName.html(`${pokemon.name}`);
    
            let imageSrc = pokemon.sprites.other['official-artwork'].front_default;
            let image = $('<img>').attr('src', imageSrc).attr('alt', 'Pokemon Image');
            modalBody.append(image);
    
            // Menambahkan kelas untuk animasi
            image.addClass('pokemon-animation');
    
            //modalBody.append(`<h5 id="stats">Statistik</h5>`);
    
            **//*//*let badge = $('#pokemonModalBody');
badge.empty();
$.each(pokemon.types, (index, val) => {
badge.append(`<span id="badge">${val.type.name}</span>`);
});**//*//*
        // radar char
        let canvas = $('<canvas>').attr('id', 'chartContainer');
    modalBody.append(canvas);
    let stats = [];
    let labels = [];

    $.each(pokemon.stats, (index, stat) => {
        stats.push(stat.base_stat);
        labels.push(stat.stat.name);
    });

    let chartContainer = document.getElementById('chartContainer');
    let chart = new Chart(chartContainer, {
        type: 'radar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Stats',
                data: stats,
                backgroundColor: 'rgba(255, 99, 132, 0.4)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1

            }]
        },
        options: {
            scales: {
                r: {
                    beginAtZero: true,
                    pointLabels: {
                        font: {
                            color: 'blue',
                            size: 14, // Ukuran font untuk label atribut
                            weight: 'bold' // Ketebalan font untuk label atribut                                
                        },
                        display: true
                    }
                }
            }
        }
    });

    $('#pokemonModal').modal('show');
});
}
*/



// table pokemon
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon?limit=1000&offset=0",
}).done((hasil) => {
    // variabel data menampung data dari api 
    let data = [];
    $.each(hasil.results, (key, val) => {
        data.push([
            `${key + 1}.`,
            val.name,
            val.url,
            `<button class="btn btn-primary btn-deskripsi" data-toggle="modal" data-bs-target="#employeeModal" onclick="openEmployeeModal()">Deskripsi </button>`,
            '<button class="btn btn-success btn-employee" data-toggle="modal" data-bs-target="#employeeModal" onclick="openEmployeeModal()">Registration </button>'

        ]);
    });
    // data table
    $(document).ready(function () {
        $('#pokemons-table').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            data: data,
            columns: [
                { title: 'NO' },
                { title: 'NAME' },
                { title: 'URL' },
                { title: 'ACTION' },
                { title: 'EMPLOYEE' }
            ]
        });
        // button deskripsi
        $('pokemon-table').on('click', '.btn-deskripsi', function () {
            openEmployeeModal();
        });

        $('pokemon-table').on('click', '.btn-employee', function () {
            openEmployeeModal();
        });

    });
});


//employee modal
function openEmployeeModal() {
    $('#employeeModal').modal('show');
}

function closeModal() {
    $('#employeeModal').modal('hide');
}
function submitForm() {
    $('#formEmployee').submit(function () {
        let inputs = $('.form-control');
        let isValid = true;
        inputs.each(function () {
            if (inputs == "") {
                isValid = false;
                return false;
            }
        });
        if (!isValid) {
            alert('There is an unfilled form');
            event.preventDefault();
        }

    });
}


// validation birtdate
var today = new Date();
var month = String(today.getMonth() + 1).padStart(2, '0');
var day = String(today.getDate()).padStart(2, '0');
var year = today.getFullYear();
var todayFormatted = year + '-' + month + '-' + day;

$('#employee-birthdate').attr('max', todayFormatted);


//confirm password
$(document).ready(function () {
    $("#formEmployee").submit(function (event) {
        let password = $("#employee-password").val();
        let confirmPassword = $("#employee-confirmPassword").val();
        if (password !== confirmPassword) {
            event.preventDefault();
            alert("password and confirmpassword do not match");
        }
    })
})

//slider
$(document).ready(function () {
    let slider = document.querySelector("#employee-skillEnglish");
    let awal = document.querySelector(".awal");
    let akhir = document.querySelector(".akhir");

    awal.innerHTML = slider.min;
    slider.addEventListener('input', function () {
        akhir.innerHTML = slider.value;
    })
})
