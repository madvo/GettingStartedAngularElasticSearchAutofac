app.controller("DeleteComponentController", function ($scope, $location, $route, ShareData, CRUDService) {

    getComponent();
    function getComponent() {
        //get component by id
        var promiseGetComponent = CRUDService.getComponent(ShareData.value);

        promiseGetComponent.then(function (pl) {
            $scope.Component = pl.data;
        },
            function (errorPl) {
                $scope.error = 'failure loading Component', errorPl;
            });
    }
    //delete method
    $scope.delete = function () {
        var promiseDeleteComponent = CRUDService.delete(ShareData.value);

            $location.path("/showComponents");
  
        },
            function (errorPl) {
                $scope.error = 'failure loading Component', errorPl;
          
    };


});