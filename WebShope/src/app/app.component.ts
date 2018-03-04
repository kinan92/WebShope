import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Product } from './Product';


@Component({
    selector: 'my-app',
    templateUrl: `./app.component.html`
})
export class AppComponent implements OnInit {
    name = 'Angular';
    AuthStatus = false;
    newProduct: Product = { Id: 4, Name: "Name", Info: "Info", Price: 0, Image: "Image" };

    products: Product[];

    selecteimage: Product;
    constructor(private http: Http) { }

    ngOnInit(): void
    {
        this.GetProduct();
        this.GetAuthStatus();
    }

    onSelect(selectProduct: Product) {
        
        if (this.selecteimage == selectProduct) {
            this.selecteimage = null;
        } else {
            this.selecteimage = selectProduct;
        }       
    }

    GetProduct() {
        this.http.get('/Home/ProductList')
            .subscribe(data => {
                this.products = data.json();
            });

    }


    GetAuthStatus() {
        this.http.get('/Home/GetAuthStatus')
            .subscribe(data => {
                this.AuthStatus = data.json();
            });

    }

    public onClick(product: Product): void {
        console.log("cart");
        this.http.post('/Carts/AddToCart', product)
            .subscribe(data => {

                console.log(data);
                if (data.status == 200) {
                    
                }

            });  

    }

    Product() {
        

        console.log(this.newProduct); 
        this.http.post('/Home/CreateProduct', this.newProduct)
            .subscribe(data => {
 
                console.log(data);
                if (data.status == 200)
                {
                    this.products.push(data.json());
                }
               
            });     
    }


}



