app.controller('AddComponentController', function ($scope, $location, $route, CRUDService) {
    $scope.Id = 0;
   //save component data
    $scope.save = function () {
   
        var Component = {
            Id: $scope.Id,
            Name: $scope.Name,
            Price: $scope.Price === undefined ? "" : $scope.Price.replace(/^0+(?=\d)/, ''),
            Type: $scope.Type,
            Country: $scope.Country,
            DueDate: $scope.DueDate
        };
        var validation = CRUDService.validate(Component);

        //validate before saving
        if (validation ==="") {
            var promisePost = CRUDService.post(Component);

            promisePost.then(function (pl) {
                
                $location.path("/showComponents");
            },
                function (errorPl) {
                    $scope.error = 'failure loading Component', errorPl;
                });
        } else {

            $scope.error = validation;
        }
       
    };

});