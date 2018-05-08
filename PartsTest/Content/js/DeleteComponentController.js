app.controller("DeleteComponentController", function ($scope, $location, $route, ShareData, SinglePageCRUDService) {

    getComponent();
    function getComponent() {
        //get component by id
        var promiseGetComponent = SinglePageCRUDService.getComponent(ShareData.value);

        promiseGetComponent.then(function (pl) {
            $scope.Component = pl.data;
        },
            function (errorPl) {
                $scope.error = 'failure loading Component', errorPl;
            });
    }
    //delete method
    $scope.delete = function () {
        var promiseDeleteComponent = SinglePageCRUDService.delete(ShareData.value);

            $location.path("/showComponents");
  
        },
            function (errorPl) {
                $scope.error = 'failure loading Component', errorPl;
          
    };


});