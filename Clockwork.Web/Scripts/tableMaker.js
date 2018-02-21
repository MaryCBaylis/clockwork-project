var TableMaker = (function(){
    var recordsPerPage;
    var dataArray;
    var displayHeaders;
    var currentPage;

//quick and dirty- this assumes an array of JSON has been passed in
//and each object has the exact same properties- no error checking built in!
    function init(inDataArray, inDisplayHeaders, inRecordsPerPage) {
        dataArray = inDataArray;
        displayHeaders = inDisplayHeaders;
        recordsPerPage = inRecordsPerPage;
        currentPage = 1;
    }

    function make(requestedPage = 1) {
        currentPage = requestedPage;
        var html = getTopBorder(dataArray.length);
        html += getTableHeaders(dataArray[0], displayHeaders);
        html += getDataRows(dataArray);
        html += getCloseTags();
        html += getBottomBorder();
        return html;
    }

    function getTopBorder() {
        var numberOfRecords = dataArray.length;
        var currentRangeLow = (currentPage - 1) * recordsPerPage + 1;
        var currentRangeHigh = Math.min(currentPage * recordsPerPage, dataArray.length);
        var result = `<div class="top-border">Displaying ${currentRangeLow} through ${currentRangeHigh} of ${numberOfRecords} records</div>`;
        return result;
    }

    function getTableHeaders(item, displayHeaders) {
        var keys = Object.keys(item);

        var result = `<div class="table-responsive">\n    <table class="table table-striped table-hover">\n        <tr>`;

        keys.forEach(function(header){
            result += `\n            <th scope="col">${displayHeaders[header]}</th>`
        })

        result += `\n        </tr>`

        return result;
    }

    function getCloseTags() {
        var result = "</table></div>";
        return result;
    }

    function getDataRows(dataArray) {
        var result = "";
        var currentRangeLow = (currentPage - 1) * recordsPerPage;
        var currentRangeHigh = currentPage * recordsPerPage - 1;

        for(var i = currentRangeLow; i < currentRangeHigh + 1; i++){
            var item = dataArray[i];
            if (item) {
                result += `        <tr scope="row" class="hover-color">`;
                Object.keys(item).forEach(function(key){
                    var data = getFormattedItem(item, key);
                    result += `            <td>${data}</td>`;
                })
                result += `        </tr>`;
            }
        }

        return result;
    }

    function getBottomBorder() {
        var result = `<div class="bottom-border">`;
        var numberOfPages = Math.ceil(dataArray.length / recordsPerPage);

        //Dirty pagination at bottom of page
        if (recordsPerPage < dataArray.length)
        {
            result += `<span class="page-request ${1 === currentPage ? 
            "current-page" : ""}" data-page-request="1">1</span>`;
            if (numberOfPages < 6){
                for (var i = 2; i < numberOfPages; i++) {
                    result += `&nbsp&nbsp<span class="page-request ${i === 
                    currentPage ? "current-page" : ""}" 
                    data-page-request="${i}">${i}</span>`;
                }
            } else if (numberOfPages - currentPage < 2) {
                if (currentPage > 1) {
                    result += " ... ";
                }
                for (var i = numberOfPages - 3; i < numberOfPages + 1; i++) {
                    result += `&nbsp&nbsp<span class="page-request ${i === currentPage ? "current-page" : ""}" data-page-request="${i}">${i}</span>`;
                }
            } else {
                if (currentPage > 1) {
                    result += " ... ";
                }
                var currentLow = Math.max(currentPage - 2, 2);
                var currentHigh = currentPage + 2, numberOfPages;
                for (var i = currentLow; i < currentHigh + 1; i++){
                    result += `&nbsp&nbsp<span class="page-request ${i === currentPage ? "current-page" : ""}" data-page-request="${i}">${i}</span>`;
                }
                if (currentPage < numberOfPages - 2) {
                    result += " ... ";
                    result += `<span class="page-request ${numberOfPages === currentPage ? "current-page" : ""}" data-page-request="${numberOfPages}">${numberOfPages}</span>`
                }
            }
        }

        result += `</div>`;

        return result;
    }

    function getFormattedItem(item, key) {
        var result;
        if (key === "time" || key === "utcTime") {
            result = new Date(Date.parse(item[key], "yyyy-MM-dd HH:mm z"));
            result = getDisplayDate(result);
        } else {
            result = item[key];
        }
        return result;
    }

    function getDisplayDate(date) {
        var seconds = fillSingleDigitSpace(date.getSeconds());
        var minutes = fillSingleDigitSpace(date.getMinutes());
        var result = 
            date.toDateString() + " " +
            date.getHours() + ":" +
            minutes + ":" +
            seconds;

        return result;
    }

    function fillSingleDigitSpace(number) {
        var result = number < 10 ? "0" + number : number;
        return result;
    }

    return {
        init: init,
        make: make
    }
})();