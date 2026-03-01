import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
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
  cargando: boolean = false;
  guardando: boolean = false;

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
    private categoriaService: CategoriaService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.cargarProductos();
    this.cargarCategorias();
  }

  cargarProductos(): void {
    this.cargando = true;
    this.productoService.getAll().subscribe({
      next: (data) => {
        this.productos = data;
        this.productosFiltrados = data;
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }

  cargarCategorias(): void {
    this.categoriaService.getAll().subscribe({
      next: (data) => {
        this.categorias = data;
      }
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
    if (this.guardando) return;
    this.guardando = true;

    this.productoForm.idCategoria = Number(this.productoForm.idCategoria);
    this.productoForm.precio = Number(this.productoForm.precio);
    this.productoForm.stock = Number(this.productoForm.stock);

    if (this.modoEdicion) {
      this.productoService.update(this.productoForm).subscribe({
        next: () => {
          console.log('actualizado');
          this.cargarProductos();
          this.limpiarFormulario();
          this.guardando = false;
        },
        error: (err) => {
          console.log('error', err);
          this.guardando = false;
        }
      });
    } else {
      this.productoService.create(this.productoForm).subscribe({
        next: () => {
          console.log('creado');
          this.cargarProductos();
          this.limpiarFormulario();
          this.guardando = false;
        },
        error: (err) => {
          console.log('error', err);
          this.guardando = false;
        }
      });
    }
  }

  eliminar(id: number): void {
    if (confirm('¿Estás seguro de eliminar este producto?')) {
      this.cargando = true;
      this.productoService.delete(id).subscribe({
        next: () => {
          this.cargarProductos();
        },
        error: () => {
          this.cargando = false;
        }
      });
    }
  }
}