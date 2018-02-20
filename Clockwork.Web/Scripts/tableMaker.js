var TableMaker = (function(){

//quick and dirty- this assumes an array of JSON has been passed in
//and each object has the exact same properties- no error checking built in!
    function make(dataArray, displayHeaders) {
        var html = getTopBorder(dataArray.length);
        html += getTableHeaders(dataArray[0], displayHeaders);
        html += getDataRows(dataArray);
        html += getCloseTags();
        html += getBottomBorder();
        return html;
    }

    function getTopBorder(numberOfRecords) {
        var result = `<div class="top-border">${numberOfRecords} Records Found</div>`;
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

        for(var i = 0; i < dataArray.length; i++){
            result += `        <tr scope="row" class="hover-color">`;
            var item = dataArray[i];
            Object.keys(item).forEach(function(key){
                var data = getFormattedItem(item, key);
                result += `            <td>${data}</td>`;
            })
            result += `        </tr>`;
        }

        return result;
    }

    function getBottomBorder() {
        var result = `<div class="bottom-border">Displaying all Records</div>`;
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
        var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
        var result = 
            date.toDateString() + " " +
            date.getHours() + ":" +
            date.getMinutes() + ":" + 
            seconds;

        return result;
    }

    return {
        make: make
    }
})();