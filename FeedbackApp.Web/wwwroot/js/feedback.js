$(document).ready(function () {
    GetFeedbacks();
    LoadCustomerNamesFilter();
    LoadCategoriesFilter();

    $('#btnSearch').click(function () {
        SearchFeedbacks();
    });

    $("#datepicker").datepicker({
        dateFormat: "dd-mm-yy"
    });
});

function GetFeedbacks() {
    $.ajax({
        url: '/Feedback/GetFeedbacks',
        type: 'GET',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="5">' + 'No feedbacks found.' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object)
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.customerName + '</td>';
                    object += '<td>' + item.category + '</td>';
                    object += '<td>' + item.description + '</td>';
                    object += '<td>' + item.submissionDate + '</td>';
                    object += '<td> <a href=# class="btn btn-primary btn-sm" onclick="Edit(\'' + item.id + '\')">Edit</a> <a href=# class="btn btn-danger btn-sm" onclick="Delete(\'' + item.id + '\')">Delete</a> </td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object)
            }
        },
        error: function () {
            alert('Unable to read the data.');
        }
    });
}

$('#btnAdd').click(function () {
    $('#FeedbackModal').modal('show');
    $('#modalTitle').text('Add Feedback');
})

function Insert() {
    if (!IsValidData()) {
        return false;
    }

    var feedbackData = new Object();
    feedbackData.Id = $('#Id').val();
    feedbackData.CustomerName = $('#CustomerName').val();
    feedbackData.Category = $('#Category').val();
    feedbackData.Description = $('#Description').val();

    $.ajax({
        url: '/Feedback/Insert',
        type: 'POST',
        data: feedbackData,
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save the data.');
            }
            else {
                HideModal();
                ClearData();
                GetFeedbacks();
                LoadCustomerNamesFilter();
                LoadCategoriesFilter();
                alert(response);
            }
        },
        error: function () {
            alert('Unable to read the data.');
        }
    })
}

function HideModal() {
    $('#FeedbackModal').modal('hide');
}

function ClearData() {
    $('#CustomerName').val('');
    $('#Category').val('');
    $('#Description').val('');

    $('#CustomerName').css('border-color', 'lightgray');
    $('#Category').css('border-color', 'lightgray');
    $('#Description').css('border-color', 'lightgray');
}

function IsValidData() {
    var isValid = true;

    if ($('#CustomerName').val().trim() == "") {
        $('#CustomerName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CustomerName').css('border-color', 'lightgrey');
    }

    if ($('#Category').val().trim() == "") {
        $('#Category').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Category').css('border-color', 'lightgrey');
    }

    if ($('#Description').val().trim() == "") {
        $('#Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }

    return isValid;
}

$('#CustomerName').change(function () {
    IsValidData();
})

$('#Category').change(function () {
    IsValidData();
})

$('#Description').change(function () {
    IsValidData();
})

function Edit(feedbackId) {
    var urlEdit = '/Feedback/Edit?id=' + feedbackId;
    $.ajax({
        url: urlEdit,
        type: 'GET',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Unable to get the data.');
            }
            else if (response.length == 0) {
                alert('Data not available for the Id: ' + id);
            }
            else {
                $('#FeedbackModal').modal('show');
                $('#modalTitle').text('Update Feedback');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#Id').val(response.id);
                $('#CustomerName').val(response.customerName);
                $('#Category').val(response.category);
                $('#Description').val(response.description);
            }
        },
        error: function () {
            alert('Unable to read the data.');
        }
    })
}

function Update() {
    var result = IsValidData();
    if (result == false) {
        return false;
    }

    var feedbackData = new Object();
    feedbackData.Id = $('#Id').val();
    feedbackData.CustomerName = $('#CustomerName').val();
    feedbackData.Category = $('#Category').val();
    feedbackData.Description = $('#Description').val();

    $.ajax({
        url: '/Feedback/Update',
        type: 'POST',
        data: feedbackData,
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save the data.');
            }
            else {
                HideModal();
                ClearData();
                GetFeedbacks();
                LoadCustomerNamesFilter();
                LoadCategoriesFilter();
                alert(response);
            }
        },
        error: function () {
            alert('Unable to save the data.');
        }
    })
}

function Delete(feedbackId) {
    if (confirm('Do you confirm the delete action?')) {
        var urlEdit = '/Feedback/Delete?id=' + feedbackId;
        $.ajax({
            url: urlEdit,
            type: 'POST',
            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Unable to delete the data.');
                }
                else {
                    GetFeedbacks();
                    LoadCustomerNamesFilter();
                    LoadCategoriesFilter();
                    alert(response);
                }
            },
            error: function () {
                alert('Unable to delete the data.');
            }
        })
    }
}

function LoadCustomerNamesFilter() {
    $.ajax({
        url: '/Feedback/GetCustomerNames',
        type: 'GET',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#customerNameFilter').find('option:not(:first)').remove();

                $.each(response, function (index, customer) {
                    var option = $('<option>').val(customer).text(customer);
                    $('#customerNameFilter').append(option);
                });
            } else {
                $('#customerNameFilter').find('option:not(:first)').remove();
            }
        },
        error: function () {
            alert('Unable to read the customer names data.');
        }
    });
}

function LoadCategoriesFilter() {
    $.ajax({
        url: '/Feedback/GetCategories',
        type: 'GET',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#categoryFilter').find('option:not(:first)').remove();

                $.each(response, function (index, customer) {
                    var option = $('<option>').val(customer).text(customer);
                    $('#categoryFilter').append(option);
                });
            } else {
                $('#categoryFilter').find('option:not(:first)').remove();
            }
        },
        error: function () {
            alert('Unable to read the customer names data.');
        },
        error: function () {
            alert('Unable to read the categories data.');
        }
    });
}

function SearchFeedbacks() {
    var customerName = $('#customerNameFilter').val();
    var category = $('#categoryFilter').val();
    var startDate = $('#fromDate').val();
    var endDate = $('#toDate').val();

    // Realizar la búsqueda con los filtros aplicados
    $.ajax({
        url: '/Feedback/SearchFeedbacks',
        type: 'GET',
        data: {
            customerName: customerName,
            category: category,
            startDate: startDate,
            endDate: endDate
        },
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="5">' + 'No feedbacks found.' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object)
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.customerName + '</td>';
                    object += '<td>' + item.category + '</td>';
                    object += '<td>' + item.description + '</td>';
                    object += '<td>' + item.submissionDate + '</td>';
                    object += '<td> <a href=# class="btn btn-primary btn-sm" onclick="Edit(\'' + item.id + '\')">Edit</a> <a href=# class="btn btn-danger btn-sm" onclick="Delete(\'' + item.id + '\')">Delete</a> </td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object)
            }
        },
        error: function () {
            alert('Unable to filter the data.');
        }
    });
}

$(function () {
    var from = $("#fromDate")
        .datepicker({
            dateFormat: "yy-mm-dd",
            changeMonth: true
        })
        .on("change", function () {
            to.datepicker("option", "minDate", getDate(this));
        }),
        to = $("#toDate").datepicker({
            dateFormat: "yy-mm-dd",
            changeMonth: true
        })
            .on("change", function () {
                from.datepicker("option", "maxDate", getDate(this));
            });

    function getDate(element) {
        var date;
        var dateFormat = "yy-mm-dd";
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }

        return date;
    }
});
