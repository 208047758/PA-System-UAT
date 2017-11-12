var Quicktrips = (function () {
    var app = {
        "getDropDownData": function () {
        },
        "getDropDownDataByid": function () {
        }
    };

    var testfunc = function () {
    };

    function test() {
    }

    return app;
})();

var greetings = (function () {
    function greetings(greeting, greets, empnumber) {
        this.greeting = greeting;
        this.greets = greets;
        this.empnumber = empnumber;
    }
    greetings.prototype.greet = function () {
        return "awe" + this.greeting;
    };
    return greetings;
})();

var g = new greetings("yes", "awe", 4);

g.greet();
