TZDropDownMaker = (function(){
    function make(timeZoneList) {
        var html = `<select id="timezone-select" class="">`;

        var regions = Object.keys(timeZoneList).sort();
        regions.forEach(function(regionName){
            html += `\n    <optgroup label=${regionName}>`;
            var region = timeZoneList[regionName];
            var zones = Object.keys(region).sort();
            zones.forEach(function(zone){
                var id = region[zone];
                html += `\n        <option value="${id}">${zone}</option>`;
            })
        })

        html+="</select>"

        return html;
      
    }

    return {
        make: make
    }
})();
