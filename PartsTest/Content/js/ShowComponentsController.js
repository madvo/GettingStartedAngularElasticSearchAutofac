app.controller('ShowComponentsController', function ($scope, $location, $timeout, $route, CRUDService, ShareData) {


    loadRecords();

    //Function to Load all Components Records.   
    function loadRecords() {
        
        var promiseGetComponents = CRUDService.getComponents();

        promiseGetComponents.then(function (response) {
        $scope.Components = response.data;
  
        },        
            function (errorPl) {
                $scope.error = 'failure loading Component', errorPl;
            });   
    }


    //Method to route to the addComponent
    $scope.addComponent = function () {
        $location.path("/addComponent");
    }

    //Method to route to the editComponent
    $scope.editComponent = function (Id) {
        ShareData.value = Id;
        $location.path("/editComponent");
    }

    //Method to route to the deleteComponent
    $scope.deleteComponent = function (Id) {
        ShareData.value = Id;
        $location.path("/deleteComponent");
    }
});