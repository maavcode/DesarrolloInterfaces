using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Modelos;

public partial class JardineriaContext : DbContext
{
    public JardineriaContext()
    {
    }

    public JardineriaContext(DbContextOptions<JardineriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Detallepedido> Detallepedidos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Gamasproducto> Gamasproductos { get; set; }

    public virtual DbSet<Oficina> Oficinas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().
                          UseMySQL("server=127.0.0.1;port=3306;database=jardineria;user=root;password=mysql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CodigoCliente).HasName("PRIMARY");

            entity.HasOne(d => d.CodigoEmpleadoRepVentasNavigation).WithMany(p => p.Clientes).HasConstraintName("Clientes_EmpleadosFK");
        });

        modelBuilder.Entity<Detallepedido>(entity =>
        {
            entity.HasKey(e => new { e.CodigoPedido, e.CodigoProducto }).HasName("PRIMARY");

            entity.HasOne(d => d.CodigoPedidoNavigation).WithMany(p => p.Detallepedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DetallePedidos_PedidoFK");

            entity.HasOne(d => d.CodigoProductoNavigation).WithMany(p => p.Detallepedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DetallePedidos_ProductoFK");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.CodigoEmpleado).HasName("PRIMARY");

            entity.HasOne(d => d.CodigoJefeNavigation).WithMany(p => p.InverseCodigoJefeNavigation).HasConstraintName("Empleados_EmpleadosFK");

            entity.HasOne(d => d.CodigoOficinaNavigation).WithMany(p => p.Empleados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Empleados_OficinasFK");
        });

        modelBuilder.Entity<Gamasproducto>(entity =>
        {
            entity.HasKey(e => e.Gama).HasName("PRIMARY");
        });

        modelBuilder.Entity<Oficina>(entity =>
        {
            entity.HasKey(e => e.CodigoOficina).HasName("PRIMARY");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => new { e.CodigoCliente, e.Idtransaccion }).HasName("PRIMARY");

            entity.HasOne(d => d.CodigoClienteNavigation).WithMany(p => p.Pagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pagos_clienteFK");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.CodigoPedido).HasName("PRIMARY");

            entity.HasOne(d => d.CodigoClienteNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pedidos_Cliente");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.CodigoProducto).HasName("PRIMARY");

            entity.HasOne(d => d.GamaNavigation).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Productos_gamaFK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
