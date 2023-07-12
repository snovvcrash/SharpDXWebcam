// ------------------------------------------------------------------
// DirectX.Capture
//
// History:
//	2003-Jan-24		BL		- created
//
// Copyright (c) 2003 Brian Low
// ------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using DShowNET;

namespace DirectX.Capture
{

	/// <summary>
	///  Specify the frequency of the TV tuner.
	/// </summary>
	public enum TunerInputType
	{
		/// <summary> Cable frequency </summary>
		Cable,
		/// <summary> Antenna frequency </summary>
		Antenna
	}


	/// <summary>
	///  Control and query a hardware TV Tuner.
	/// </summary>
	public class Tuner : IDisposable
	{
		// ---------------- Private Properties ---------------

		protected IAMTVTuner tvTuner = null;		



		// ------------------- Constructors ------------------

		/// <summary> Initialize this object with a DirectShow tuner </summary>
		public Tuner(IAMTVTuner tuner)
		{
			tvTuner = tuner;
		}



		// ---------------- Public Properties ---------------
		
		/// <summary>
		///  Get or set the TV Tuner channel.
		/// </summary>
		public int Channel
		{
			get
			{
				int channel;
				int v, a;
				int hr = tvTuner.get_Channel( out channel, out v, out a );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return( channel );
			}

			set
			{
				int hr = tvTuner.put_Channel( value, AMTunerSubChannel.Default, AMTunerSubChannel.Default );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		/// <summary>
		///  Get or set the tuner frequency (cable or antenna).
		/// </summary>
		public TunerInputType InputType
		{
			get
			{
				DShowNET.TunerInputType t;
				int hr = tvTuner.get_InputType( 0, out t );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return( (TunerInputType) t );
			}
			set
			{
				DShowNET.TunerInputType t = (DShowNET.TunerInputType) value;
				int hr = tvTuner.put_InputType( 0, t );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		/// <summary>
		///  Indicates whether a signal is present on the current channel.
		///  If the signal strength cannot be determined, a NotSupportedException
		///  is thrown.
		/// </summary>
		public bool SignalPresent
		{
			get
			{
				AMTunerSignalStrength sig;
				int hr = tvTuner.SignalPresent( out sig );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				if ( sig == AMTunerSignalStrength.NA ) throw new NotSupportedException("Signal strength not available.");
				return( sig == AMTunerSignalStrength.SignalPresent );
			}
		}

		// ---------------- Public Methods ---------------

		public void Dispose()
		{
			if ( tvTuner != null )
				Marshal.ReleaseComObject( tvTuner ); tvTuner = null;
		}
	}
}
