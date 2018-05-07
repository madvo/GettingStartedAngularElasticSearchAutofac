app.service("SinglePageCRUDService", function ($http, $q) {

    //Function to read all component data
    this.getComponents = function () {
        return $http.get("api/ComponentAPI/");
       
    };

    //Function to get component data by id
    this.getComponent = function (id) {
        return $http.get("/api/ComponentAPI/" + id);
    };

    //Function to create new component
    this.post = function (Component) {
        var request = $http({
            method: "post",
            url: "/api/ComponentAPI",
            data: Component
        });
        return request;
    };
    //Function to edit component
    this.put = function (id, Component) {
        var request = $http({
            method: "put",
            url: "/api/ComponentAPI/" + id,
            data: Component
        });
        return request;
    };
    //Function to delete component
    this.delete = function (id) {
        var request = $http.delete("/api/ComponentAPI/" + id);
        return request;

    };

    //Function to validate data
    this.validate = function (Component) {
        var Error = "";

        var isDecimalRegEx = /^\s*(\+|-)?((\d+(\,\d+)?)|(\,\d+))\s*$/;

        function isDecimal(s) {
            return String(s).search(isDecimalRegEx) !== -1
        }

        //name is obligatory
        if (Component.Name === null || Component.Name === undefined || Component.Name === "") {
            Error+='Providing Name of the component is mandatory. ';         
        }
        //Price must be a number separated with comma
        if ((!isDecimal(Component.Price)) && Component.Price!="") {
       
            Error += 'Price must be a number.';    
        }
      
        return Error;
    };
});







