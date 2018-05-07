app.controller('ShowComponentsController', function ($scope, $location, $timeout, $route, SinglePageCRUDService, ShareData, SafeApply) {


    loadRecords();

    //Function to Load all Components Records.   
    function loadRecords() {
        
        var promiseGetComponents = SinglePageCRUDService.getComponents();

        promiseGetComponents.then(function (response) {
        $scope.Components = response.data;
        //console.log(response.data);
        //console.log(response);
       
        },        
            function (errorPl) {
                $scope.error = 'failure loading Component', errorPl;
            });   
    }

    //$scope.positive = function (name) {

    //    $scope.showHover = true;
    //    $scope.tooltipText = name;
    //}   

    //$scope.negative = function (name) {
    //    $scope.showHover = false;
    //}

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