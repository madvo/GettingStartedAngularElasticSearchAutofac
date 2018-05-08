app.controller("EditComponentController", function ($scope, $location, $route, ShareData, SinglePageCRUDService) {

    getComponent();
    function getComponent() {

        var promiseGetComponent = SinglePageCRUDService.getComponent(ShareData.value);
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
        var validation = SinglePageCRUDService.validate(Component);

        //validate before saving
        if (validation === "") {
        var promisePutComponent = SinglePageCRUDService.put($scope.Component.Id, Component);
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