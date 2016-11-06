//const app = angular.module('app', []);//set and get the angular module
//app.controller('productController', ['$scope', '$http', productController]);

//// Start from filter
//app.filter('startFrom', function () {
//    return function (input, start) {
//        if (!input || !input.length) { return; }
//        start = +start; //parse to int
//        return input.slice(start);
//    }
//});

//function productController($scope, $http){

//    $scope.loading = true;
//    $scope.addMode = false;

//    //get all product information
//    $http.get('/api/product/').success(function (data) {
//        $scope.products = data;
//        $scope.loading = false;
//    }).error(function (error) {
//        $scope.error = "An Error has occured while loading posts!";
//        $scope.loading = false;
//        });


//    // Pagination
//    $scope.currentPage = 0;
//    $scope.pageSize = 10;
//    $scope.setCurrentPage = function (currentPage) {
//        $scope.currentPage = currentPage;
//    }

//    $scope.getNumberAsArray = function (num) {
//        return new Array(num);
//    };

//    $scope.numberOfPages = function () {
//        return Math.ceil($scope.products.length / $scope.pageSize);
//    };
//}
