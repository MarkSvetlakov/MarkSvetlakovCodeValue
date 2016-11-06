const mainApp = angular.module('app', ['ngAnimate', 'ngSanitize', 'ui.bootstrap', 'ui.router']);

mainApp.config(($stateProvider, $urlRouterProvider) => {
    $stateProvider.state('addItems', {
        url: '/addItems',
        template: `<selectors></selectors>
                    <search-products></search-products>
                    <cart-result></cart-result>`
    })
        .state('compareItem', {
            url: '/compareItem',
            template: '<compare-result></compare-result>',
        });

    $urlRouterProvider.otherwise('/addItems');
});



mainApp.service('storeFactory', function () {
    var stores = [];
    return {
        getStores: function () {
            return stores;
        },
        pushToStores: function (data) {
            if (stores.indexOf(data) == -1) {
                stores.push(data);
            }
        },
        removeFromStores: function (index) {
            stores.splice(index, 1);
        },
        clearData: function () {
            stores = [];
        }
    }
});

mainApp.service('chainFactory', function () {
    var chains = [];
    var show;
    return {
        showOption: function (value) {
            show = value;
        },
        isVisible: function () {
            return show;
        },
        getChains: function () {
            return chains;
        },
        pushToChains: function (data) {
            if (chains.indexOf(data) == -1) {
                chains.push(data);
            }
        },
        removeFromChains: function (index) {
            chains.splice(index, 1);
        },
        clearData: function () {
            chains = [];
        }
    };
});

mainApp.controller('chainController', ['$scope', '$http', 'chainFactory', 'storeFactory', chainController]);

function chainController($scope, $http, chainFactory, storeFactory) {

    this.push = function (store, chain) {
        storeFactory.pushToStores(store);
        chainFactory.pushToChains(chain);
    }
    $scope.stores = storeFactory.getStores();
    $http.get('/api/chain/').success(function (data) {
        $scope.show = function () { chainFactory.showOption(true); };
        $scope.visible = function () { chainFactory.isVisible(); };
        $scope.chains = data;
        $scope.filter1 = function (item) {
            return (!($scope.selectedStore1 && $scope.selectedStore1.store_name) || item.store_name != $scope.selectedStore1.store_name);
        };
        $scope.filter2 = function (item) {
            return (!($scope.selectedStore2 && $scope.selectedStore2.store_name) || item.store_name != $scope.selectedStore2.store_name);
        };

        //$scope.filter3 = function (item) {
        //    return (!($scope.selectedStore3 && $scope.selectedStore3.store_name) || item.store_name != $scope.selectedStore3.store_name);
        //};
    }).error(function (error) {
        });
}


mainApp.controller('productController', ['$scope', '$http', 'arrayFactory', '$state', 'storeFactory', productController]);

function productController($scope, $http, arrayFactory, $state, storeFactory) {
    this.$state = $state;
    $http.get('/api/product/').success(function (data) {
        $scope.products = data;
        $scope.quantity = 1;
        $scope.clear = function () {
            $scope.search = undefined;
        };
        $scope.addToCart = function (data) {
            data.product.quantity = data.quantity;
            arrayFactory.pushToArray(data);
        };

        $scope.getCart = function () {
            arrayFactory.getArray();
        };

        $scope.filter = function (item) {
            var store = storeFactory.getStores()[0];

            if (store) {
                var i;
                for (i = 0; i < item.stores_products.length; i++) {
                    if (item.stores_products[i].store_id == store.store_id) {
                        return item.stores_products[i];
                    }
                }
            }
            else {
                return undefined;
            }
        };
    }).error(function (error) {
        });

    this.selectedStores = function () {
        if (storeFactory.getStores().length > 1) {
            return false;
        }
        else {
            return true;
        }
    };
};

productController.prototype.allowCompare = function allowCompare() {
    this.$state.go('compareItem');
};


mainApp.controller('cartController', ['$scope', 'arrayFactory', cartController]);

function cartController($scope, arrayFactory) {
    $scope.itemsInCart = arrayFactory.getArray();
    $scope.removeFromCart = function (data) {
        var index = $scope.itemsInCart.indexOf(data);
        arrayFactory.removeFromArray(index);
    }
}

class HeaderController {
    static $inject: string[] = ['$state', 'storeFactory', 'chainFactory'];
    constructor(private state: any, private storeFactory: any, private chainFactory: any) {
    }

    public goHome() {
        this.state.go('addItems');
        this.storeFactory.clearData();
        this.chainFactory.clearData();
    };
}
mainApp.controller('headerController', HeaderController);


class ResultController {
    static $inject: string[] = ['arrayFactory', 'storeFactory', 'chainFactory', '$log'];
    result: number;
    Data: IResult[];
    ProductArray: Array<IProduct>;
    tempResult: IResult;
    tempProduct: IProduct;
    testChain: string;

    constructor(private arrayFactory: any, private storeFactory: any, private chainFactory: any, private log: ng.ILogService) {
        this.GetData();
    }

    public SetClass(bool) {
        if (bool) {
            return "cheapest";
        }
        else {
            return "dark";
        }
    }

    public SetContainerClass(bool) {
        if (bool) {
            return "container-compare-result-cheapest";
        }
        else {
            return "container-compare-result";
        }
    }

    public GetStores() {
        return this.storeFactory.getStores();
    }

    public CheckPrice(data, price) {
        if (data.LowerPrice == price) {
            return 'lower-price';
        }
        if (data.HigherPrice == price) {
            return 'higher-price';
        }
    }

    public GetData() {
        var chains = this.chainFactory.getChains();
        var stores = this.storeFactory.getStores();
        var products = this.arrayFactory.getArray();
        this.Data = new Array<IResult>();

        for (var i = 0; i < stores.length; i++) {
            this.ProductArray = new Array<IProduct>();
            var store = stores[i];
            for (var j = 0; j < products.length; j++) {
                var product = products[j];

                for (var k = 0; k < product.product.stores_products.length; k++) {
                    var store_product = product.product.stores_products[k];
                    if (store_product.store_id == store.store_id) {
                        this.tempProduct = {
                            Product_Name: product.product.product_name,
                            Product_Price: store_product.product_price,
                            Product_Quantity : product.product.quantity
                        }
                        this.ProductArray.push(this.tempProduct);
                    }
                }
            }


            for (var j = 0; j < chains.length; j++) {
                var chain = chains[j];
                if (chain.stores[0].store_id == store.store_id) {
                    this.testChain = chain.chain_name;
                }
            }

            var maximumPrice = this.ProductArray[0].Product_Price;
            var minimumPrice = this.ProductArray[0].Product_Price;
            var total = 0;
            for (var j = 0; j < this.ProductArray.length; j++) {
                var tempProduct = this.ProductArray[j];
                if (maximumPrice < tempProduct.Product_Price) {
                    maximumPrice = tempProduct.Product_Price;
                }
                if (minimumPrice > tempProduct.Product_Price) {
                    minimumPrice = tempProduct.Product_Price;
                }
                total += parseFloat((tempProduct.Product_Price * tempProduct.Product_Quantity).toString());
            }

            
            this.tempResult = {
                ChainName: this.testChain,
                StoreName: store.store_name,
                Products: this.ProductArray,
                TotalPrice: total,
                IsCheapest: false,
                HigherPrice: maximumPrice,
                LowerPrice: minimumPrice
            }
            this.Data.push(this.tempResult);
        }

        var minimum = this.Data[0].TotalPrice;
        for (var i = 0; i < this.Data.length; i++) {
            var data = this.Data[i];
            if (minimum > data.TotalPrice) {
                minimum = data.TotalPrice;
            }
        }

        for (var i = 0; i < this.Data.length; i++) {
            var data = this.Data[i];
            if (minimum == data.TotalPrice) {
                this.Data[i].IsCheapest = true;
                this.log.log(this.Data[i].StoreName);
            }
        }
    }
}
mainApp.controller('resultController', ResultController);

mainApp.service('arrayFactory', function () {
    var array = [];
    return {
        getArray: function () {
            return array;
        },
        pushToArray: function (data) {
            if (array.indexOf(data) == -1) {
                array.push(data);
            }
        },
        removeFromArray: function (index) {
            array.splice(index, 1);
        }
    }
});

interface IResult {
    ChainName: string;
    StoreName: string;
    Products: Array<IProduct>;
    TotalPrice: number;
    IsCheapest: boolean;
    LowerPrice: number;
    HigherPrice: number;
}

interface IProduct {
    Product_Name: string;
    Product_Price: number;
    Product_Quantity: number;
}