export interface Producto {
  idProducto: number;
  idCategoria: number;
  nombreCategoria?: string;
  nombre: string;
  precio: number;
  stock: number;
  estado: string;
  fechaCreacion?: string;
}
