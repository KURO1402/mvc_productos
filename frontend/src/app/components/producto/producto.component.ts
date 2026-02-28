import { Component, OnInit } from '@angular/core';
import { Producto } from '../../models/producto.model';
import { Categoria } from '../../models/categoria.model';
import { ProductoService } from '../../services/producto.service';
import { CategoriaService } from '../../services/categoria.service';

@Component({
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css'],
  standalone: false
})
export class ProductoComponent implements OnInit {

  productos: Producto[] = [];
  categorias: Categoria[] = [];
  productosFiltrados: Producto[] = [];
  busqueda: string = '';
  modoEdicion: boolean = false;

  productoForm: Producto = {
    idProducto: 0,
    idCategoria: 0,
    nombre: '',
    precio: 0,
    stock: 0,
    estado: 'A'
  };

  constructor(
    private productoService: ProductoService,
    private categoriaService: CategoriaService
  ) { }

  ngOnInit(): void {
    this.cargarProductos();
    this.cargarCategorias();
  }

  cargarProductos(): void {
    this.productoService.getAll().subscribe(data => {
      this.productos = data;
      this.productosFiltrados = data;
    });
  }

  cargarCategorias(): void {
    this.categoriaService.getAll().subscribe(data => {
      this.categorias = data;
    });
  }

  filtrar(): void {
    this.productosFiltrados = this.productos.filter(p =>
      p.nombre.toLowerCase().includes(this.busqueda.toLowerCase())
    );
  }

  editar(producto: Producto): void {
    this.modoEdicion = true;
    this.productoForm = { ...producto };
  }

  limpiarFormulario(): void {
    this.modoEdicion = false;
    this.productoForm = {
      idProducto: 0,
      idCategoria: 0,
      nombre: '',
      precio: 0,
      stock: 0,
      estado: 'A'
    };
  }

  guardar(): void {
    if (this.modoEdicion) {
      this.productoService.update(this.productoForm).subscribe(() => {
        this.cargarProductos();
        this.limpiarFormulario();
      });
    } else {
      this.productoService.create(this.productoForm).subscribe(() => {
        this.cargarProductos();
        this.limpiarFormulario();
      });
    }
  }

  eliminar(id: number): void {
    if (confirm('¿Estás seguro de eliminar este producto?')) {
      this.productoService.delete(id).subscribe(() => {
        this.cargarProductos();
      });
    }
  }
}
