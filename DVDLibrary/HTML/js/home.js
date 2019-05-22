// JavaScript source code for DVD Library Exercise
$(document).ready(function () {

    loadDvds();

    $('#createDvdbutton').click(function (event) {
        
        $('#topRow').hide();
        $('#dvdTableDiv').hide();
        $('#addDvdDiv').show();
        })

    $('#create-button').click(function (event) {

            // check for errors and display any that we have
            // pass the input associated with the add form to the validation function
            var haveValidationErrors = checkAndDisplayValidationErrors($('#create-form').find('input'));

            // if we have errors, bail out by returning false
            if (haveValidationErrors) {
                return false;
            }

            // if we made it here, there are no errors so make the ajax call

            $.ajax({
                type: 'POST',
                url: 'http://localhost:64269/dvd',
                data: JSON.stringify({
                    title: $('#create-dvd-title').val(),
                    releaseYearName: $('#create-release-year').val(),
                    directorName: $('#create-director').val(),
                    ratingType: $('#create-rating').val(),
                    notes: $('#create-notes').val()
                }),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                'dataType': 'json',
                success: function () {
                    // clear errorMessages
                    $('#errorMessages').empty();
                    // Clear the form and reload the table
                    $('#create-dvd-title').val('');
                    $('#create-release-year').val('');
                    $('#create-director').val('');
                    $('#create-rating').val('');
                    $('#create-notes').val('');
                    loadDvds();

                    $('#topRow').show();
                    $('#dvdTableDiv').show();
                    $('#addDvdDiv').hide();
                },
                error: function () {
                    $('#errorMessages')
                   .append($('<li>')
                   .attr({ class: 'list-group-item list-group-item-danger' })
                   .text('Error calling web service.  Please try again later.'));
                }
        });
    });

    $('#create-cancel-button').click(function (event) {
        $('#errorMessages').empty();
        $('#create-dvd-title').val('');
        $('#create-release-year').val('');
        $('#create-director').val('');
        $('#create-rating').val('');
        $('#create-notes').val('');
        $('#topRow').show();
        $('#dvdTableDiv').show();
        $('#addDvdDiv').hide();
    });
    
    $('#search-button').click(function (event) {
        var searchTermInput = $('#searchTermInput').val();
        var drpDownSelection = $('#searchCategorySelect').val();
         
        var haveValidationErrors = checkAndDisplayValidationErrors($('#search-form').find('input'));

        if (haveValidationErrors) {
            return false;
        }

        // if we made it here, there are no errors so make the ajax call
        
        if (drpDownSelection == "Title")
        {
            loadDvdsByTitle(searchTermInput)
        }
        if (drpDownSelection == "Release Year") {
            loadDvdsByYear(searchTermInput)
        }
        if (drpDownSelection == "Director Name") {
            loadDvdsByDirector(searchTermInput)
        }
        if (drpDownSelection == "Rating") {
            loadDvdsByRating(searchTermInput)
        }
  
    });

    // Update Button onclick handler
    $('#edit-button').click(function (event) {

        // check for errors and display any that we have
        // pass the input associated with the edit form to the validation function
        var haveValidationErrors = checkAndDisplayValidationErrors($('#edit-form').find('input'));

        // if we have errors, bail out by returning false
        if (haveValidationErrors) {
            return false;
        }
        
        // if we get to here, there were no errors, so make the Ajax call
        $.ajax({
            type: 'PUT',
            url: 'http://localhost:64269/dvd/' + $('#edit-dvd-id').val(),
            data: JSON.stringify({
                id: $('#edit-dvd-id').val(),
                title: $('#edit-dvd-title').val(),
                releaseYearName: $('#edit-release-year').val(),
                directorName: $('#edit-director').val(),
                ratingType: $('#edit-rating').val(),
                notes: $('#edit-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function () {
                // clear errorMessages
                $('#errorMessages').empty();
                hideEditForm();
                loadDvds();
            },
            error: function () {
                $('#errorMessages')
                   .append($('<li>')
                   .attr({ class: 'list-group-item list-group-item-danger' })
                   .text('Error calling web service.  Please try again later.'));
            }
        })
    });
});

function loadDvds() {

    clearDvdTable();

    var contentRows = $('#contentRows');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:64269/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseYear = dvd.releaseYearName;
                var director = dvd.directorName;
                var rating = dvd.ratingType;
                var id = dvd.dvdId;

                var row = '<tr>';
                row += '<td style="text-align: center"><a onclick="showDetails(' + id + ')">' + title + '</td>';
                row += '<td style="text-align: center">' + releaseYear + '</td>';
                row += '<td style="text-align: center">' + director + '</td>';
                row += '<td style="text-align: center">' + rating + '</td>';
                row += '<td style="text-align: center"><a onclick="showEditForm(' + id + ')">Edit</a> | <a onclick="deleteDvd(' + id + ')">Delete</a></td>';
                
                row += '</tr>';

                contentRows.append(row);
            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Error calling web service.  Please try again later.'));
        }
    });
}

function loadDvdsByTitle(searchTermInput) {
    
    clearDvdTable();

    var contentRows = $('#contentRows');
    var noMatches = true;    
    //loop dvds, if dvd.title = title, add to contentrows
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64269/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseYear = dvd.releaseYearName;
                var director = dvd.directorName;
                var rating = dvd.ratingType;
                var id = dvd.dvdId;

                var row = '<tr>';
                row += '<td style="text-align: center"><a onclick="showDetails(' + id + ')">' + title + '</td>';
                row += '<td style="text-align: center">' + releaseYear + '</td>';
                row += '<td style="text-align: center">' + director + '</td>';
                row += '<td style="text-align: center">' + rating + '</td>';
                row += '<td style="text-align: center"><a onclick="showEditForm(' + id + ')">Edit</a> | <a onclick="deleteDvd(' + id + ')">Delete</a></td>';

                row += '</tr>';
                
                if(dvd.title.includes(searchTermInput))
                {
                    console.log('Equal: Checking if ' + dvd.title + ' includes ' + $('#searchTermInput').val());
                    contentRows.append(row);
                    noMatches = false;
                }
             });
            if (noMatches) {
                displaySearchErrorMessage();
            }
            
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Error calling web service.  Please try again later.'));
        }
    });
}

function displaySearchErrorMessage(message)
{
    $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text('No matches found. Please enter another selection.'));
    console.log('made it into displaySearchErrorMessage');
}

function loadDvdsByYear(year) {

    clearDvdTable();

    var contentRows = $('#contentRows');
    var noMatches = true;
    //loop dvds, if dvd.title = title, add to contentrows
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64269/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseYear = dvd.releaseYearName;
                var director = dvd.directorName;
                var rating = dvd.ratingType;
                var id = dvd.dvdId;

                var row = '<tr>';
                row += '<td style="text-align: center"><a onclick="showDetails(' + id + ')">' + title + '</td>';
                row += '<td style="text-align: center">' + releaseYear + '</td>';
                row += '<td style="text-align: center">' + director + '</td>';
                row += '<td style="text-align: center">' + rating + '</td>';
                row += '<td style="text-align: center"><a onclick="showEditForm(' + id + ')">Edit</a> | <a onclick="deleteDvd(' + id + ')">Delete</a></td>';

                row += '</tr>';

                if (releaseYear == year) {
                    contentRows.append(row);
                    noMatches = false;
                }
            });
            if(noMatches)
            {
                displaySearchErrorMessage();
            }
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Error calling web service.  Please try again later.'));
        }
    });
}

function loadDvdsByDirector(directorName) {

    clearDvdTable();

    var contentRows = $('#contentRows');
    var noMatches = true;
    //loop dvds, if dvd.title = title, add to contentrows
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64269/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseYear = dvd.releaseYearName;
                var director = dvd.directorName;
                var rating = dvd.ratingType;
                var id = dvd.dvdId;

                var row = '<tr>';
                row += '<td style="text-align: center"><a onclick="showDetails(' + id + ')">' + title + '</td>';
                row += '<td style="text-align: center">' + releaseYear + '</td>';
                row += '<td style="text-align: center">' + director + '</td>';
                row += '<td style="text-align: center">' + rating + '</td>';
                row += '<td style="text-align: center"><a onclick="showEditForm(' + id + ')">Edit</a> | <a onclick="deleteDvd(' + id + ')">Delete</a></td>';

                row += '</tr>';

                if (dvd.directorName.includes(directorName)) {
                    contentRows.append(row);
                    noMatches = false;
                }
            });
            if(noMatches)
            {
                displaySearchErrorMessage();
            }
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Error calling web service.  Please try again later.'));
        }
    });
}

function loadDvdsByRating(ratingType) {

    clearDvdTable();

    var contentRows = $('#contentRows');
    var noMatches = true;
    //loop dvds, if dvd.title = title, add to contentrows
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64269/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseYear = dvd.releaseYearName;
                var director = dvd.directorName;
                var rating = dvd.ratingType;
                var id = dvd.dvdId;

                var row = '<tr>';
                row += '<td style="text-align: center"><a onclick="showDetails(' + id + ')">' + title + '</td>';
                row += '<td style="text-align: center">' + releaseYear + '</td>';
                row += '<td style="text-align: center">' + director + '</td>';
                row += '<td style="text-align: center">' + rating + '</td>';
                row += '<td style="text-align: center"><a onclick="showEditForm(' + id + ')">Edit</a> | <a onclick="deleteDvd(' + id + ')">Delete</a></td>';

                row += '</tr>';

                if (rating == ratingType) {
                    contentRows.append(row);
                    noMatches = false;
                }
            });
            if(noMatches)
            {
                displaySearchErrorMessage()
            }
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Error calling web service.  Please try again later.'));
        }
    });
}

function clearDvdTable() {
    $('#contentRows').empty();
}

function showEditForm(dvdId) {
    $('#errorMessages').empty();

    // get the dvd details from the server and then fill and show the
    // form on success
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64269/dvd/' + dvdId,
        success: function (data, status) {
            $('#edit-dvd-title').val(data.title);
            $('#edit-release-year').val(data.releaseYearName);
            $('#edit-director').val(data.directorName);
            $('#edit-rating').val(data.ratingType);
            $('#edit-notes').val(data.notes);
            $('#edit-dvd-id').val(data.dvdId);
            console.log("This is the dvdId: " + dvdId);
            console.log("This is the dvdReleaseYear: " + data.releaseYearName);
            console.log("This is the dvdDirectorName: " + data.directorName);
            console.log("This is the dvdRatingType: " + data.ratingType);
            console.log("This is the dvdNotes: " + data.notes);
            console.log("This is the dvdTitle: " + data.title);
        },
        error: function () {
            $('#errorMessages')
               .append($('<li>')
               .attr({ class: 'list-group-item list-group-item-danger' })
               .text('Error calling web service.  Please try again later.'));
        }
    });
    $('#topRow').hide();
    $('#dvdTableDiv').hide();
    $('#editDvdDiv').show();
 }

function hideEditForm() {
    // clear errorMessages
    $('#errorMessages').empty();
    // clear the form and then hide it
    $('#edit-dvd-title').val('');
    $('#edit-release-year').val('');
    $('#edit-director').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');
    $('#editDvdDiv').hide();
    $('#topRow').show();
    $('#dvdTableDiv').show();
}

function showDetails(dvdId) {
    $('#showDetailsDiv').empty();

    var showDetails = $('#showDetailsDiv');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:64269/dvd/' + dvdId,
        success: function (dvd) {
            var title = dvd.title;
            var releaseYear = dvd.releaseYearName;
            var director = dvd.directorName;
            var rating = dvd.ratingType;
            var id = dvd.dvdId;
            var notes = dvd.notes;

            var row = '<br/>';
            row += '<h2>' + title + '<h2>';
            row += '<hr />';
            row += '<h4>Release Year: ' + releaseYear + '</h4>';
            row += '<h4>Director: ' + director + '</h4>';
            row += '<h4>Rating: ' + rating + '</h4>';
            row += '<h4>Notes: ' + notes + '</h4>';
            row += '<button type="button" id="details-back-button" class="btn btn-default" onclick="hideDetails()">Back</button>';
            
            showDetails.append(row);
        },
        error: function () {
            $('#errorMessages')
               .append($('<li>')
               .attr({ class: 'list-group-item list-group-item-danger' })
               .text('Error calling web service.  Please try again later.'));
        }
    });
    $('#topRow').hide();
    $('#dvdTableDiv').hide();
    $('#showDetailsDiv').show
    
}

function hideDetails() {
    $('#showDetailsDiv').hide();
    $('#dvdTableDiv').show();
}

// processes validation errors for the given input.  returns true if there
// are validation errors, false otherwise
function checkAndDisplayValidationErrors(input) {
    // clear displayed error message if there are any
    $('#errorMessages').empty();
    // check for HTML5 validation errors and process/display appropriately
    // a place to hold error messages
    var errorMessages = [];

    // loop through each input and check for validation errors
    input.each(function () {
        // Use the HTML5 validation API to find the validation errors
        if (!this.validity.valid) {

            //var errorField = $('label[for=' + this.id + ']').text();
           
           errorMessages.push(this.validationMessage);
        }
    });

    // put any error messages in the errorMessages div
    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        // return true, indicating that there were errors
        return true;
    } else {
        // return false, indicating that there were no errors
        return false;
    }
}

function deleteDvd(dvdId) {

    const modal = document.querySelector('dialog');

    // makes modal appear (adds `open` attribute)
    modal.showModal();
    
    $('#confirmDelete').click(function (event) {
        $.ajax({
            type: 'DELETE',
            url: "http://localhost:64269/dvd/" + dvdId,
            success: function (status) {
                location.reload();
                modal.close();
                loadDvds();
             }
        });
     })
 
    $('#cancelDelete').click(function (event) {
        location.reload();
        console.log("reloaded from cancel!!");
        modal.close();
  
    })
}
