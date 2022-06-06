$(function () {
    $('form').submit(async e => {
        e.preventDefault();

        const q = $('#search').val();

        var r = await fetch('/Reviews/Search?query='+q);
        var d = await r.json();

        const template = $('#template').html();
        
        let res = '';
        for (var review of d) {
            let row = template;
            for (var key in review) {
                // converts ISO string to c# datetime
                var rep = (key == "date") ? new Date(review[key]) : review[key];
                if (key == "date") {
                    var year = rep.getFullYear();
                    var month = (rep.getMonth() + 1);
                    month = ((month > 9) ? '' : "0") + month;
                    var date = rep.getDate();
                    date = ((date > 9) ? '' : "0") + date;
                    var hour = rep.getHours();
                    var mins = rep.getMinutes();
                    mins = ((mins > 9) ? '' : "0") + mins;
                    var secs = rep.getSeconds();
                    secs = ((secs > 9) ? '' : "0") + secs;
                    rep = (date + "/" + month + "/" + year + " " + hour + ":" + mins + ":" + secs);
                }
                if (key == "feedback" && rep == null)
                    rep = "";
                row = row.replaceAll('{' + key + '}', rep);
                row = row.replaceAll('%7B' + key + '%7D', rep);
            }
            res += row;
        }
        $('tbody').html(res);
    })
});