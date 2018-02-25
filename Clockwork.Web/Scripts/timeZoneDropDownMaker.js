TZDropDownMaker = (function(){

    function make(timeZoneList) {
        
        var html = `<select id="timezone-select" class="">`;
        var regions = Object.keys(timeZoneList.regions).sort();

        regions.forEach(function(regionName){
            html += `\n    <optgroup label=${regionName}>`;
            var region = timeZoneList.regions[regionName];
            var zones = Object.keys(region).sort();

            zones.forEach(function(zone){
                var id = region[zone];
                if(timeZoneList.serverInfo.id === id){
                    html += `\n        <option value="${id}" selected>${zone}</option>`;
                } else {
                    html += `\n        <option value="${id}">${zone}</option>`;
                }
            })
        })

        html+="</select>"

        return html;
      
    }

    return {
        make: make
    }
})();
