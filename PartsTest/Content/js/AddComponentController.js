app.controller('AddComponentController', function ($scope, $location, $route, SinglePageCRUDService) {
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
        var validation = SinglePageCRUDService.validate(Component);

        //validate before saving
        if (validation ==="") {
            var promisePost = SinglePageCRUDService.post(Component);

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