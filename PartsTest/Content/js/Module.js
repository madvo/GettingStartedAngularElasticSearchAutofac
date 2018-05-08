var app = angular.module("ApplicationModule", ["ngRoute"]);


//The Factory used to define the value to
//Communicate and pass data across controllers

app.factory("ShareData", function () {
    return { value: 0 }
});


app.directive('calendar', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attr, ngModel) {
            $(el).datepicker({
                dateFormat: 'yy-mm-dd',
                onSelect: function (dateText) {
                    scope.$apply(function () {
                        ngModel.$setViewValue(dateText);
                    });
                }
            });
        }
    };
})

//Defining Routing
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/showComponents',
        {
            templateUrl: 'Component/ShowComponents',
            controller: 'ShowComponentsController'
        });
    $routeProvider.when('/addComponent',
        {
            templateUrl: 'Component/AddComponent',
            controller: 'AddComponentController'
        });
    $routeProvider.when("/editComponent",
        {
            templateUrl: 'Component/EditComponent',
            controller: 'EditComponentController'
        });
    $routeProvider.when('/deleteComponent',
        {
            templateUrl: 'Component/DeleteComponent',
            controller: 'DeleteComponentController'
        });
    $routeProvider.otherwise(
        {
            redirectTo: '/'
            //templateUrl: 'Component/ShowComponents',
            //controller: 'ShowComponentsController'
           
        });
    $locationProvider.html5Mode(true).hashPrefix('!')
}]);