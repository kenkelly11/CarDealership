$(document).ready(function () {

});

function getDetails(CarId) {
    window.location.replace("Details/" + CarId);
}

function purchaseVehicle(CarId) {
    window.location.replace("Sales/Purchase/" + CarId);
}

function editVehicle(CarId) {
    window.location.replace("Admin/Edit/" + CarId);
}

function deleteCar(carId) {
    if (confirm("Are you sure you want to delete this Car?")) {
        $.ajax({
            url: "https://localhost:44331/Admin/Delete/" + carId,
            type: "DELETE",

            success: function (status) {

                window.location.replace("https://localhost:44331/Admin"); // refresh the page to admin search screen
                alert("Car has been deleted from database.");
            },
            error: function () {
                $("#errorMessages").append(
                    $("<li>")
                        .attr({
                            class: "list-group-item list-group-item-danger"
                        })
                        .text("There was a problem deleting the car from the database.")
                );
            }
        });
    }
    return false;
}


function deleteSpecial(specialId) {
    if (confirm("Are you sure you want to delete this Special?")) {
        $.ajax({
            url: 'https://localhost:44331/Admin/DeleteSpecial/' + specialId,
            type: "DELETE",

            success: function (status) {
                alert("Special has been deleted from database.");
                window.location.reload();
            },
            error: function () {
                $("#errorMessages").append(
                    $("<li>")
                        .attr({
                            class: "list-group-item list-group-item-danger"
                        })
                        .text("There was a problem deleting the special from the database.")
                );
            }
        });
    }
    return false;
}
      


function setMakeId() {
    $("#MakeId").attr("value", $("#Makes").val());
}

function setCarIds() {

    // first id is from the HiddenFor elements, second id is from the dropdown lists
    $("#MakeId").attr("value", $("#Makes").val())
    $("#ModelId").attr("value", $("#Models").val())
    $("#MakeName").attr("value", $("Makes").val())
    $("#ModelName").attr("value", $("ModelName").val())
    $("#BodyStyleId").attr("value", $("#BodyStyleType").val())
    $("#TransmissionId").attr("value", $("#TransmissionType").val())
    $("#IntColorId").attr("value", $("#InteriorColor").val())
    $("#BodyColorId").attr("value", $("#BodyColor").val())
    $("#deleteId").attr("value", $("#CarId").val())
}

function getModels(makeId) {

    $.ajax({
        url: "/Admin/GetModels/" + makeId,
        type: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            var markup = "";
            for (var x = 0; x < data.length; x++) { // show all model options for the make selected
                markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#Models").html(markup).show();
        },
        error: function () {
            $("#errorMessages").append(
                $("<li>")
                    .attr({
                        class: "list-group-item list-group-item-danger"
                    })
                    .text("There was a problem getting models for this vehicle make.")
            );
        }
    });
}

function searchVehicles(vehicleSearch) {
    // we need to clear the previous content so we don't append to it
    clearVehicleSearch();

    $("#errorMessages").empty();

    $("#VehicleSearchResultsDiv").hide();


    var vehicleSearchRows = $("#vehicleSearchRows");

    var minPrice = $("#minPriceSelectBox").val();

    var maxPrice = $("#maxPriceSelectBox").val();

    var minYear = $("#minYearSelectBox").val();

    var maxYear = $("#maxYearSelectBox").val();

    var searchTerm = $("#carSearchTermInput").val();

    var searchType = vehicleSearch; // determined by which search button was clicked

    var detailsOrPurchase = callSearchType(searchType);

    var buttonLabel = determineButtonLabel(searchType);

    if (searchTerm === "") {
        searchTerm = "null";
    }

    $.ajax({
        type: "GET",
        url: "https://localhost:44331/api/inventory/" + searchType + "/" + searchTerm + "/" + minYear + "/" + maxYear + "/" + minPrice + "/" + maxPrice,
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data, status) {
            alert("Searching... ");
            $.each(data, function (index, car) {
                var CarId = car.CarId;
                var Make = car.Make;
                var Model = car.Model;
                var Year = car.Year;
                var Img = car.IMGURL;
                var IntColor = car.InteriorColor;
                var BodyColor = car.BodyColor;
                var BodyStyle = car.BodyStyle;
                var Transmission = car.Transmission;
                var Mileage = car.Mileage;
                var VIN = car.VIN;
                var SalePrice = car.SalePrice;
                var MSRP = car.MSRP;

                var row = "<tr>";
                row +=
                    '<td><ul class="vehicleResultsList"><li class="vehicleResultsListRow">' + ' ' + Year + ' ' + Make + ' ' + Model + '</li>' +
                    '<li class="vehicleResultsListRow"><img class="resize" height="200" width="200" src="' + Img + '" /></li>' +
                    '</ul ></td>';
                row += '<td><ul class="vehicleResultsList"><li class="vehicleResultsListRow">BodyStyle: ' + BodyStyle + '</li>' +
                    '<li class="vehicleResultsListRow">Trans: ' + Transmission + '</li>' +
                    '<li class="vehicleResultsListRow">Color: ' + BodyColor + '</li>' +
                    '</ul></td >';
                row += '<td><ul class="vehicleResultsList"><li class="vehicleResultsListRow">Interior: ' + IntColor + '</li>' +
                    '<li class="vehicleResultsListRow">Mileage: ' + Mileage + '</li>' +
                    '<li class="vehicleResultsListRow">VIN: ' + VIN + '</li>' +
                    '</ul></td >';
                row += '<td><ul class="vehicleResultsList"><li class="vehicleResultsListRow">Sale Price: ' + "$" + formatPrice(SalePrice, 2, '.', ',') + '</li>' +
                    '<li class="vehicleResultsListRow">MSRP: ' + '$' + formatPrice(MSRP, 2, '.', ',') + '</li>' +
                    '<li class="vehicleResultsListRow"><button type="button" id="vehicleDetailsButton" class="btn btn-default" onclick="' + detailsOrPurchase + CarId + ')"> ' + buttonLabel + '</button > ' + '</li > ' + 
                    "</ul></td >";
                row += "</tr>";
                vehicleSearchRows.append(row);
            });


        },
        error: function () {
            $("#errorMessages").append(
                $("<li>")
                    .attr({
                        class: "list-group-item list-group-item-danger"
                    })
                    .text("Your search returned no results. Please try again.")
            );
        }
    });

    $("#VehicleSearchResultsDiv").show();
}

// detailsOrPurchase
function callSearchType(searchType) {
    if (searchType === 'SearchNewCars' || searchType === 'SearchUsedCars') {
        return "getDetails(";
    }

    if (searchType === 'SalesSearchCars') {
        return "purchaseVehicle(";
    }

    if (searchType === 'AdminSearchCars') {
        return "editVehicle(";
    }
}

// changes the label of the button depending on what the user is doing with the car
function determineButtonLabel(searchType) {
    if (searchType === 'SearchNewCars' || searchType === 'SearchUsedCars') {
        return "Details";
    }

    if (searchType === 'SalesSearchCars') {
        return "Purchase";
    }

    if (searchType === 'AdminSearchCars') {
        return "Edit";
    }
}

// searching for users who made sales 
function searchSales() {
    // we need to clear the previous content so we don't append to it
    clearSalesSearch();

    $("#errorMessages").empty();

    $("#salesSearchResultsDiv").hide();

    // grab the tbody element that will hold the rows of vehicle information
    var salesSearchRows = $("#salesSearchRows");

    var minYear = $("#minYearSelectBox").val();

    var maxYear = $("#maxYearSelectBox").val();

    var searchTerm = $("#salesSearchTermInput").val();

    if (searchTerm === "") {
        searchTerm = "null";
    }

    $.ajax({
        type: "GET",
        url: "https://localhost:44331/api/reports/sales/" + searchTerm + "/" + minYear + "/" + maxYear + "/",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data, status) {
            if (data.length == 0) {
                $("#errorMessages").append(
                    $("<li>")
                        .attr({
                            class: "list-group-item list-group-item-danger"
                        })
                        .text("Your search returned no results. Please try again.")
                );
            }
            $.each(data, function (index, sale) {
                var user = sale.UserName;
                var carsSold = sale.CarsSold;
                var sales = sale.Sales;

                var row = "<tr>";
                row += '<td>' + user + '</td>';
                row += '<td>' + '$' + sales + '</td>';
                row += '<td>' + carsSold + '</td>';
                row += "</tr>";
                salesSearchRows.append(row);
            });
        },
        error: function () {
            $("#errorMessages").append(
                $("<li>")
                    .attr({
                        class: "list-group-item list-group-item-danger"
                    })
                    .text("There was a problem communicating with the server please try again.")
            );
        }
    });

    $("#salesSearchResultsDiv").show();
}

function clearSalesSearch() {
    $("#salesSearchRows").empty();
}

function toTitleCase(str) {
    return str.replace(/(?:^|\s)\w/g, function (match) {
        return match.toUpperCase();
    });
}

function formatPrice(value, decimals, decimalSeparator, thousandSeparator) {
    if (value == null || isNaN(value))
        return "";

    var decimals = isNaN(c = Math.abs(decimals)) ? 2 : decimals;
    var decimalSeparator = decimalSeparator == undefined ? "." : decimalSeparator;
    var thousandSeparator = thousandSeparator == undefined ? " " : thousandSeparator;

    var negativeSign = value < 0 ? "-" : "";

    var valueNoDecimals = String(parseInt(value = Math.abs(Number(value) || 0).toFixed(decimals)));

    var spacingStart = 0;
    if ((valueNoDecimals.length) > 3)
        spacingStart = valueNoDecimals.length % 3;

    var leadingNumber = (spacingStart ? valueNoDecimals.substr(0, spacingStart) + thousandSeparator : "");
    var separatedMiddle = valueNoDecimals.substr(spacingStart).replace(/(\d{3})(?=\d)/g, "$1" + thousandSeparator);
    var decimals = (decimals ? decimalSeparator + Math.abs(value - valueNoDecimals).toFixed(decimals).slice(2) : "");

    var result = negativeSign + leadingNumber + separatedMiddle + decimals;
    return result.trim();
};


function clearVehicleSearch() {
    $("#vehicleSearchRows").empty();
}

// slideshow functions

var slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");

    if (n > slides.length)
    {
        slideIndex = 1
    }

    if (n < 1)
    {
        slideIndex = slides.length
    }

    for (i = 0; i < slides.length; i++)
    {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++)
    {
        dots[i].className = dots[i].className.replace(" active", "");
    }

    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}



