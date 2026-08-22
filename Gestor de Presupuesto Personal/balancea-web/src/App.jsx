import { useState, useEffect } from 'react'
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, Cell } from 'recharts'
import './App.css'

const API_URL = 'https://localhost:7127/api'

function useApiList(endpoint, refrescar) {
  const [datos, setDatos] = useState([])
  const [cargando, setCargando] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    setCargando(true)
    fetch(`${API_URL}/${endpoint}`)
      .then(res => {
        if (!res.ok) throw new Error(`Error al cargar ${endpoint}`)
        return res.json()
      })
      .then(data => {
        setDatos(data)
        setCargando(false)
      })
      .catch(err => {
        setError(err.message)
        setCargando(false)
      })
  }, [endpoint, refrescar])

  return { datos, cargando, error }
}

function useEliminar(endpoint, onEliminado) {
  const [eliminandoId, setEliminandoId] = useState(null)

  const eliminar = async (id) => {
    if (!window.confirm('¿Seguro que deseas eliminar este registro? Esta accion no se puede deshacer.')) return
    setEliminandoId(id)
    try {
      const res = await fetch(`${API_URL}/${endpoint}/${id}`, { method: 'DELETE' })
      if (!res.ok) throw new Error('No se pudo eliminar')
      onEliminado()
    } catch (err) {
      alert(err.message)
    } finally {
      setEliminandoId(null)
    }
  }

  return { eliminar, eliminandoId }
}

function HeroBalance({ usuarioId, refrescar }) {
  const [balance, setBalance] = useState(null)
  const [cargando, setCargando] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    if (!usuarioId) return
    setCargando(true)
    fetch(`${API_URL}/Balance/${usuarioId}`)
      .then(res => {
        if (!res.ok) throw new Error('No se pudo calcular el balance')
        return res.json()
      })
      .then(data => {
        setBalance(data)
        setCargando(false)
      })
      .catch(err => {
        setError(err.message)
        setCargando(false)
      })
  }, [usuarioId, refrescar])

  if (!usuarioId) return null
  if (cargando) return <div className="hero-balance"><p className="estado">Calculando balance...</p></div>
  if (error) return <div className="hero-balance"><p className="error">Error: {error}</p></div>

  const esPositivo = balance.estado === 'Positivo'
  const dataGrafica = [
    { nombre: 'Ingresos', valor: balance.totalIngresos, color: '#2ECC71' },
    { nombre: 'Gastos', valor: balance.totalGastos, color: '#E8A33D' },
  ]

  return (
    <div className="hero-balance">
      <span className="hero-label">Balance actual</span>
      <span className={`hero-monto ${esPositivo ? 'positivo' : 'negativo'}`}>
        ${balance.balance}
      </span>
      <span className={`hero-badge ${esPositivo ? 'positivo' : 'negativo'}`}>
        {esPositivo ? 'Saludable' : 'En numeros rojos'}
      </span>
      <div className="hero-subcifras">
        <div>
          <span className="hero-sub-label">Ingresos</span>
          <span className="hero-sub-monto positivo">+${balance.totalIngresos}</span>
        </div>
        <div>
          <span className="hero-sub-label">Gastos</span>
          <span className="hero-sub-monto negativo">-${balance.totalGastos}</span>
        </div>
      </div>

      <div className="grafica-wrap">
        <ResponsiveContainer width="100%" height={150}>
          <BarChart data={dataGrafica} margin={{ top: 10, right: 10, left: -20, bottom: 0 }}>
            <CartesianGrid strokeDasharray="3 3" stroke="#2A333D" vertical={false} />
            <XAxis
              dataKey="nombre"
              tick={{ fill: '#F4F6F8', fontSize: 12 }}
              axisLine={{ stroke: '#2A333D' }}
              tickLine={false}
            />
            <YAxis
              tick={{ fill: '#8B95A1', fontSize: 11 }}
              axisLine={false}
              tickLine={false}
            />
            <Tooltip
              contentStyle={{ background: '#1A2128', border: '1px solid #2A333D', borderRadius: 8, fontSize: 12 }}
              labelStyle={{ color: '#F4F6F8' }}
              itemStyle={{ color: '#F4F6F8' }}
              cursor={{ fill: 'rgba(255,255,255,0.03)' }}
            />
            <Bar dataKey="valor" radius={[6, 6, 0, 0]}>
              {dataGrafica.map((entry, i) => <Cell key={i} fill={entry.color} />)}
            </Bar>
          </BarChart>
        </ResponsiveContainer>
      </div>
    </div>
  )
}

function useEnviarPost(endpoint, onCreado) {
  const [enviando, setEnviando] = useState(false)
  const [mensaje, setMensaje] = useState(null)

  const enviar = async (body) => {
    setEnviando(true)
    setMensaje(null)
    try {
      const res = await fetch(`${API_URL}/${endpoint}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      })
      if (!res.ok) throw new Error(`No se pudo crear el registro (${res.status})`)
      setMensaje({ tipo: 'ok', texto: 'Guardado correctamente.' })
      onCreado()
      return true
    } catch (err) {
      setMensaje({ tipo: 'error', texto: err.message })
      return false
    } finally {
      setEnviando(false)
    }
  }

  return { enviar, enviando, mensaje }
}

function FormUsuario({ onCreado }) {
  const [nombre, setNombre] = useState('')
  const [correo, setCorreo] = useState('')
  const { enviar, enviando, mensaje } = useEnviarPost('Usuario', onCreado)

  const submit = async (e) => {
    e.preventDefault()
    const ok = await enviar({ nombre, correo })
    if (ok) { setNombre(''); setCorreo('') }
  }

  return (
    <form className="form" onSubmit={submit}>
      <label className="form-label">Nombre completo</label>
      <input type="text" placeholder="Ej: Juan Perez" value={nombre} onChange={e => setNombre(e.target.value)} required />
      <label className="form-label">Correo electronico</label>
      <input type="email" placeholder="Ej: juan@correo.com" value={correo} onChange={e => setCorreo(e.target.value)} required />
      <button type="submit" disabled={enviando}>{enviando ? 'Guardando...' : 'Guardar usuario'}</button>
      {mensaje && <p className={mensaje.tipo === 'error' ? 'error' : 'exito'}>{mensaje.texto}</p>}
    </form>
  )
}

function FormCategoria({ onCreado }) {
  const [nombre, setNombre] = useState('')
  const [tipo, setTipo] = useState('Gasto')
  const { enviar, enviando, mensaje } = useEnviarPost('Categoria', onCreado)

  const submit = async (e) => {
    e.preventDefault()
    const ok = await enviar({ nombre, tipo })
    if (ok) setNombre('')
  }

  return (
    <form className="form" onSubmit={submit}>
      <label className="form-label">Nombre de la categoria</label>
      <div className="form-fila">
        <input type="text" placeholder="Ej: Comida, Transporte" value={nombre} onChange={e => setNombre(e.target.value)} required />
        <select value={tipo} onChange={e => setTipo(e.target.value)}>
          <option value="Gasto">Es un gasto</option>
          <option value="Ingreso">Es un ingreso</option>
        </select>
      </div>
      <button type="submit" disabled={enviando}>{enviando ? 'Guardando...' : 'Guardar categoria'}</button>
      {mensaje && <p className={mensaje.tipo === 'error' ? 'error' : 'exito'}>{mensaje.texto}</p>}
    </form>
  )
}

function FormMovimiento({ endpoint, onCreado, refrescar }) {
  const [monto, setMonto] = useState('')
  const [fecha, setFecha] = useState('')
  const [usuarioId, setUsuarioId] = useState('')
  const [categoriaId, setCategoriaId] = useState('')
  const { enviar, enviando, mensaje } = useEnviarPost(endpoint, onCreado)

  const { datos: usuarios } = useApiList('Usuario', refrescar)
  const { datos: categorias } = useApiList('Categoria', refrescar)

  const submit = async (e) => {
    e.preventDefault()
    if (!usuarioId || !categoriaId) return
    const ok = await enviar({
      monto: parseFloat(monto),
      fecha,
      usuarioId: parseInt(usuarioId),
      categoriaId: parseInt(categoriaId)
    })
    if (ok) { setMonto(''); setFecha('') }
  }

  return (
    <form className="form" onSubmit={submit}>
      <label className="form-label">Monto y fecha</label>
      <div className="form-fila">
        <input type="number" step="0.01" min="0" placeholder="Monto en $" value={monto} onChange={e => setMonto(e.target.value)} required />
        <input type="date" value={fecha} onChange={e => setFecha(e.target.value)} required />
      </div>
      <label className="form-label">Quien y en que categoria</label>
      <div className="form-fila">
        <select value={usuarioId} onChange={e => setUsuarioId(e.target.value)} required>
          <option value="" disabled>Selecciona un usuario</option>
          {usuarios.map(u => <option key={u.id} value={u.id}>{u.nombre}</option>)}
        </select>
        <select value={categoriaId} onChange={e => setCategoriaId(e.target.value)} required>
          <option value="" disabled>Selecciona una categoria</option>
          {categorias.map(c => <option key={c.id} value={c.id}>{c.nombre}</option>)}
        </select>
      </div>
      <button type="submit" disabled={enviando}>{enviando ? 'Guardando...' : 'Guardar'}</button>
      {mensaje && <p className={mensaje.tipo === 'error' ? 'error' : 'exito'}>{mensaje.texto}</p>}
    </form>
  )
}

function Seccion({ titulo, descripcion, endpoint, renderItem, refrescar, formulario, onRefrescar, categorias }) {
  const { datos, cargando, error } = useApiList(endpoint, refrescar)
  const { eliminar, eliminandoId } = useEliminar(endpoint, onRefrescar)
  const [abierto, setAbierto] = useState(false)

  return (
    <section className="seccion">
      <div className="seccion-header">
        <div>
          <h2>{titulo}</h2>
          {descripcion && <p className="seccion-desc">{descripcion}</p>}
        </div>
        <button className="btn-toggle" onClick={() => setAbierto(a => !a)}>
          {abierto ? 'Cerrar' : '+ Agregar'}
        </button>
      </div>

      {abierto && <div className="form-wrap">{formulario}</div>}

      {cargando && <p className="estado">Cargando...</p>}
      {error && <p className="error">Error: {error}</p>}
      {!cargando && !error && (
        <ul>
          {datos.length === 0 && <li className="vacio">Sin registros todavia.</li>}
          {datos.map(item => (
            <li key={item.id} title={renderItem.tooltip ? renderItem.tooltip(item) : undefined}>
              {renderItem(item)}
              <button
                className="btn-eliminar"
                onClick={() => eliminar(item.id)}
                disabled={eliminandoId === item.id}
                title="Eliminar"
              >
                {eliminandoId === item.id ? '...' : '×'}
              </button>
            </li>
          ))}
        </ul>
      )}
    </section>
  )
}

function App() {
  const [refrescar, setRefrescar] = useState(0)
  const [usuarioSeleccionado, setUsuarioSeleccionado] = useState('')
  const [tema, setTema] = useState('oscuro')
  const disparar = () => setRefrescar(r => r + 1)

  const { datos: usuarios } = useApiList('Usuario', refrescar)
  const { datos: categorias } = useApiList('Categoria', refrescar)
  const todoVacio = usuarios.length === 0

  useEffect(() => {
    document.documentElement.setAttribute('data-theme', tema)
  }, [tema])

  useEffect(() => {
    if (usuarios.length === 0) {
      setUsuarioSeleccionado('')
      return
    }
    const existeSeleccionado = usuarios.some(u => String(u.id) === String(usuarioSeleccionado))
    if (!usuarioSeleccionado || !existeSeleccionado) {
      setUsuarioSeleccionado(usuarios[0].id)
    }
  }, [usuarios, usuarioSeleccionado])

  const gastoRenderItem = g => (
    <>
      <span>{new Date(g.fecha).toLocaleDateString()}</span>
      <span className="monto" style={{ color: 'var(--ambar)' }}>-${g.monto}</span>
    </>
  )
  gastoRenderItem.tooltip = g => {
    const cat = categorias.find(c => c.id === g.categoriaId)
    return cat ? `Categoria: ${cat.nombre}` : undefined
  }

  const ingresoRenderItem = i => (
    <>
      <span>{new Date(i.fecha).toLocaleDateString()}</span>
      <span className="monto" style={{ color: 'var(--verde)' }}>+${i.monto}</span>
    </>
  )
  ingresoRenderItem.tooltip = i => {
    const cat = categorias.find(c => c.id === i.categoriaId)
    return cat ? `Categoria: ${cat.nombre}` : undefined
  }

  return (
    <div className="app">
      <div className="topbar">
        <div className="topbar-brand">
          <span className="topbar-dot"></span>
          Balancea
        </div>
        <button className="btn-tema" onClick={() => setTema(t => t === 'oscuro' ? 'claro' : 'oscuro')}>
          {tema === 'oscuro' ? 'Modo claro' : 'Modo oscuro'}
        </button>
      </div>

      <header className="header">
        <h1>Balancea</h1>
        <p>Tu gestor de presupuesto personal</p>
      </header>

      {usuarios.length > 0 && (
        <div className="selector-usuario">
          <label>Ver balance de:</label>
          <select value={usuarioSeleccionado} onChange={e => setUsuarioSeleccionado(e.target.value)}>
            {usuarios.map(u => <option key={u.id} value={u.id}>{u.nombre}</option>)}
          </select>
        </div>
      )}

      <HeroBalance usuarioId={usuarioSeleccionado} refrescar={refrescar} />

      {todoVacio && (
        <div className="bienvenida">
          <p>Aun no hay datos registrados.</p>
          <p className="bienvenida-sub">Agrega tu primer usuario para comenzar a usar Balancea.</p>
        </div>
      )}

      <main className="content">
        <Seccion
          titulo="Usuarios"
          descripcion="Personas que usan el sistema"
          endpoint="Usuario"
          refrescar={refrescar}
          onRefrescar={disparar}
          formulario={<FormUsuario onCreado={disparar} />}
          renderItem={u => <><span>{u.nombre}</span><span className="dato-sec" title={u.correo}>{u.correo}</span></>}
        />

        <Seccion
          titulo="Categorias"
          descripcion="Clasifica tus gastos e ingresos"
          endpoint="Categoria"
          refrescar={refrescar}
          onRefrescar={disparar}
          formulario={<FormCategoria onCreado={disparar} />}
          renderItem={c => <><span>{c.nombre}</span><span className="dato-sec">{c.tipo}</span></>}
        />

        <Seccion
          titulo="Gastos"
          descripcion="Dinero que sale"
          endpoint="Gasto"
          refrescar={refrescar}
          onRefrescar={disparar}
          formulario={<FormMovimiento endpoint="Gasto" onCreado={disparar} refrescar={refrescar} />}
          renderItem={gastoRenderItem}
          categorias={categorias}
        />

        <Seccion
          titulo="Ingresos"
          descripcion="Dinero que entra"
          endpoint="Ingreso"
          refrescar={refrescar}
          onRefrescar={disparar}
          formulario={<FormMovimiento endpoint="Ingreso" onCreado={disparar} refrescar={refrescar} />}
          renderItem={ingresoRenderItem}
          categorias={categorias}
        />
      </main>

      <footer className="footer">
        Balancea &middot; Tu dinero, bajo control
      </footer>
    </div>
  )
}

export default App