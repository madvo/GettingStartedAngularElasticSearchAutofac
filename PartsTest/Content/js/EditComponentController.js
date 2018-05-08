app.controller("EditComponentController", function ($scope, $location, $route, ShareData, CRUDService) {

    getComponent();
    function getComponent() {

        var promiseGetComponent = CRUDService.getComponent(ShareData.value);
        promiseGetComponent.then(function (pl) {
            $scope.Component = pl.data;
        },
            function (errorPl) {
                $scope.error = 'failure loading Component', errorPl;
            });
    }


    //save component data
    $scope.save = function () {
        var Component = {
            Id: $scope.Component.Id,
            Name: $scope.Component.Name,
            Price: $scope.Component.Price,
            Type: $scope.Component.Type,
            Country: $scope.Component.Country,
            DueDate: $scope.Component.DueDate
        };
        var validation = CRUDService.validate(Component);

        //validate before saving
        if (validation === "") {
        var promisePutComponent = CRUDService.put($scope.Component.Id, Component);
        promisePutComponent.then(function (pl) {
          
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