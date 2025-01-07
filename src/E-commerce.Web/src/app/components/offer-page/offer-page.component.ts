import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../_services/product.service';
import { Product } from '../../types/Product';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from "../home-page/home-page.component";
import { NavbarComponent } from "../navbar/navbar.component";
import { FooterComponent } from "../footer/footer.component";

@Component({
  selector: 'app-offer-page',
  standalone: true,
  imports: [CommonModule, HomePageComponent, NavbarComponent, FooterComponent],
  templateUrl: './offer-page.component.html',
})
export class OfferPageComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  offerId: string = '';
  product: Product | undefined;

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.offerId = params.get('id') ?? '';
      },
    });
    this.productService.getProductById(this.offerId).subscribe({
      next: (product) => {
        const transformedData: Product = {
          ...product,
          additionalProperties:
            typeof product.additionalProperties === 'string'
              ? JSON.parse(product.additionalProperties)
              : product.additionalProperties,
        };
        console.log(transformedData);
        this.product = transformedData;
      },
      error: (error) => {
        console.error(error);
      },
    });
  }
}
